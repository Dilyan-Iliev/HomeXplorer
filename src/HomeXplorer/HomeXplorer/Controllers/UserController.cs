namespace HomeXplorer.Controllers
{
    using System.Text;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity.UI.Services;

    using HomeXplorer.Common;
    using HomeXplorer.Config.Google;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.User;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Exceptions;
    using HomeXplorer.Services.Exceptions.Contracts;

    using static HomeXplorer.Config.SMTP.SmtpConstants;
    using static HomeXplorer.Common.DataConstants.ApplicationUserConstants;

    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository repo;
        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        private readonly GoogleCaptchaConfig googleCaptchaService;
        private readonly IEmailSender emailSender;
        private readonly IGuard guard;

        public UserController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRepository repo,
            ICountryService countryService,
            ICityService cityService,
            GoogleCaptchaConfig googleCaptchaService,
            IEmailSender emailSender,
            IGuard guard)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.repo = repo;
            this.countryService = countryService;
            this.cityService = cityService;
            this.googleCaptchaService = googleCaptchaService;
            this.emailSender = emailSender;
            this.guard = guard;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return this.View(new RegisterViewModel()
            {
                Countries = await this.countryService.GetCountriesAsync()
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var countries = await this.countryService.GetCountriesAsync();
                model.Countries = countries;
                return this.View(model);
            }

            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            await this.CheckForRoleAsync(model.Role);

            ApplicationUser? userWithSameEmail = await userManager.FindByEmailAsync(user.Email);

            if (userWithSameEmail != null)
            {
                this.TempData["TakenEmail"] = "User with this email already exists";
                model.Countries = await this.countryService.GetCountriesAsync();
                return this.View(model);
            }

            var result = await this.userManager.CreateAsync(user, model.Password);

            await this.userManager.AddToRoleAsync(user, model.Role);

            if (result.Succeeded)
            {
                await this.CreateUserTypeAsync(model, user);

                await this.repo.SaveChangesAsync();

                return this.RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return this.View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //Verify Response Token with google
            var googleCaptchaResult = await this.googleCaptchaService.VerifyToken(model.Token);

            if (!googleCaptchaResult)
            {
                return this.View(model);
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            ApplicationUser user = await this.userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result =
                    await this.signInManager
                    .PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    string userEmail = model.Email;

                    var roles = new[] { UserRoleConstants.Renter, UserRoleConstants.Agent, UserRoleConstants.Administrator };
                    var role = roles
                        .FirstOrDefault(r => this.userManager.IsInRoleAsync(user, r).Result);

                    //this.TempData["SuccessLogin"] = $"Welcome {userEmail}";

                    return this.RedirectToAction("Index", "Home", new { area = role });
                }
            }

            this.ModelState.AddModelError("InvalidLogin", "Wrong email and/or password");

            string? errorMessage = this.ModelState["InvalidLogin"]?.Errors[0]?.ErrorMessage;
            model.ErrorMessage = errorMessage ?? string.Empty;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgottenPassword()
        {
            return this.View(new ForgottenPasswordViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgottenPassword(ForgottenPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                ApplicationUser user = await userManager.FindByEmailAsync(model.Email);

                guard.AgainstNull(user, "The user was not found by this email");

                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    var userId = user.Id;

                    var callbackUrl = $"{Request.Scheme}://{Request.Host}/User/ResetPassword?userId={userId}&token={encodedToken}";

                    await emailSender.SendEmailAsync(user.Email, "Password reset",
                        string.Format(ForgottenPasswordTemplate, callbackUrl));

                    TempData["SuccessResetRequest"] = "Please check your email in order to reset your password";
                }

                return RedirectToAction(nameof(ForgottenPassword));
            }
            catch (HomeXplorerException he)
            {
                ModelState.AddModelError("InvalidEmail", he.Message);

                string? errorMessage = ModelState["InvalidEmail"]?.Errors[0]?.ErrorMessage;
                model.ErrorMessage = errorMessage ?? string.Empty;

                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid password reset token");
                return View("NotFound");
            }

            var model = new ResetPasswordViewModel
            {
                UserId = userId,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.UserId);

            if (user != null)
            {
                var tokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                var token = Encoding.UTF8.GetString(tokenBytes);

                var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                {
                    this.TempData["SuccessReset"] = "You have successfully changed your password";
                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        private async Task CreateUserTypeAsync(RegisterViewModel model, ApplicationUser user)
        {
            if (model.Role == UserRoleConstants.Agent)
            {
                Agent agent = new Agent()
                {
                    UserId = user.Id,
                    CityId = model.CityId,
                    ProfilePictureUrl = DefaultUserProfilePictureUrl
                };

                await this.repo.AddAsync<Agent>(agent);
            }
            else if (model.Role == UserRoleConstants.Renter)
            {
                Renter renter = new Renter()
                {
                    UserId = user.Id,
                    CityId = model.CityId,
                    ProfilePictureUrl = DefaultUserProfilePictureUrl
                };

                await this.repo.AddAsync<Renter>(renter);
            }
        }

        private async Task CheckForRoleAsync(string role)
        {
            bool roleExists = await this.roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}