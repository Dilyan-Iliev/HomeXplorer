namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Profile;
    using HomeXplorer.Services.Exceptions.Contracts;
    using HomeXplorer.Core.Contexts;

    public class ProfileService
        : IProfileService
    {
        private readonly HomeXplorerDbContext dbContext;
        private readonly IGuard guard;

        public ProfileService(
            HomeXplorerDbContext dbContext,
            IGuard guard)
        {
            this.dbContext = dbContext;
            this.guard = guard;
        }

        public async Task<AgentProfileViewModel> GetAgentProfileInfoAsync(string userId)
        {
            Agent? agent = await FindAgentAsync(userId);

            this.guard.AgainstNull(agent, "No agent was found");

            var model = await this.dbContext
                .Agents
                .AsNoTracking()
                .Where(a => a.Id == agent!.Id)
                .Select(a => new AgentProfileViewModel()
                {
                    City = a.City.Name,
                    Country = a.City.Country.Name,
                    Email = a.User.Email,
                    PhoneNumber = a.User.PhoneNumber,
                    FullName = $"{a.User.FirstName} {a.User.LastName}",
                    TotalUploadedProperties = a.Properties.Count(),
                    TotalRentedProperties = a.Properties
                        .Where(p => p.Renter != null)
                        .Count(),
                    PersonalImage = a.ProfilePictureUrl,
                    PropertyImages = a.Properties
                        .OrderByDescending(p => p.AddedOn)
                        .Take(4)
                        .SelectMany(p => p.Images)
                            .Select(i => new ProfilePropertiesViewModel()
                            {
                                Id = i.Id,
                                Url = i.Url,
                                PropertyId = i.PropertyId!.Value
                            })
                        //.Take(1)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return model!;
        }

        public async Task<RenterProfileViewModel> GetRenterProfileInfoAsync(string userId)
        {
            Renter? renter = await this.FindRenterAsync(userId);

            this.guard.AgainstNull(renter, "No renter was found");

            var model = await this.dbContext
                .Renters
                .AsNoTracking()
                .Where(r => r.Id == renter!.Id)
                .Select(r => new RenterProfileViewModel()
                {
                    City = r.City.Name,
                    Country = r.City.Country.Name,
                    Email = r.User.Email,
                    PhoneNumber = r.User.PhoneNumber,
                    FullName = $"{r.User.FirstName} {r.User.LastName}",
                    PersonalImage = r.ProfilePictureUrl,
                    TotalLikedProperties = r.FavouriteProperties!.Count(),
                    TotalRentedProperties = r.RentedProperties!.Count(),
                    TotalReviews = r.Reviews.Where(rv => rv.ReviewCreatorId == r.Id).Count(),
                    AddedReviews = r.Reviews
                        .Where(rv => rv.ReviewCreatorId == r.Id)
                        .Select(rv => new ProfileReviewViewModel()
                        {
                            AddedOn = rv.AddedOn.ToString("MM/dd/yyyy"),
                            Description = rv.Description /*$"{rv.Description.Substring(0, 10)}..."*/
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return model!;
        }

        public async Task UpdateAgentProfilePictureAsync(string userId, string profilePictureUrl)
        {
            Agent? agent = await FindAgentAsync(userId);

            this.guard.AgainstNull(agent, "No agent was found");

            agent!.ProfilePictureUrl = profilePictureUrl;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateRenterProfilePictureAsync(string userId, string profilePictureUrl)
        {
            Renter? renter = await FindRenterAsync(userId);

            this.guard.AgainstNull(renter, "No agent was found");

            renter!.ProfilePictureUrl = profilePictureUrl;

            await this.dbContext.SaveChangesAsync();
        }

        private async Task<Agent?> FindAgentAsync(string userId)
        {
            return await this.dbContext
                            .Agents
                            .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        private async Task<Renter?> FindRenterAsync(string userId)
        {
            return await this.dbContext
                            .Renters
                            .FirstOrDefaultAsync(a => a.UserId == userId);
        }
    }
}