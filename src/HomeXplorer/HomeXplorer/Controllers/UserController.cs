namespace HomeXplorer.Controllers
{
    using HomeXplorer.Common;
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly HomeXplorerDbContext context; //TODO - switch with repository

        public UserController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            HomeXplorerDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return this.View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            await this.CheckForRoleAsync(model.Role);

            await this.userManager.AddToRoleAsync(user, model.Role);

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.CreateUserTypeAsync(model, user);

                await this.context.SaveChangesAsync();

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
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login");
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction(nameof(Login));
        }

        private async Task CreateUserTypeAsync(RegisterViewModel model, ApplicationUser user)
        {
            if (model.Role == UserRoleConstants.Agent)
            {
                Agent agent = new Agent()
                {
                    UserId = user.Id,
                };

                await this.context.Agents.AddAsync(agent);
            }
            else if (model.Role == UserRoleConstants.Renter)
            {
                Renter renter = new Renter()
                {
                    UserId = user.Id,
                };

                await this.context.Renters.AddAsync(renter);
            }
        }

        private async Task CheckForRoleAsync(string role)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
