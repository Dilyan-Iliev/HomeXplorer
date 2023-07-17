namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Profile;
    using HomeXplorer.Services.Exceptions.Contracts;

    public class ProfileService
        : IProfileService
    {
        private readonly IRepository repo;
        private readonly IGuard guard;

        public ProfileService(
            IRepository repo,
            IGuard guard)
        {
            this.repo = repo;
            this.guard = guard;
        }

        public async Task<AgentProfileViewModel> GetAgentProfileInfoAsync(string userId)
        {
            Agent? agent = await FindAgentAsync(userId);

            this.guard.AgainstNull(agent, "No agent was found");

            var model = await this.repo
                .AllReadonly<Agent>()
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
                                PropertyId = i.PropertyId.Value
                            })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<RenterProfileViewModel> GetRenterProfileInfoAsync(string userId)
        {
            Renter? renter = await this.FindRenterAsync(userId);

            this.guard.AgainstNull(renter, "No renter was found");

            var model = await this.repo
                .AllReadonly<Renter>()
                .Where(r => r.Id == renter!.Id)
                .Select(r => new RenterProfileViewModel()
                {
                    City = r.City.Name,
                    Country = r.City.Country.Name,
                    Email = r.User.Email,
                    PhoneNumber = r.User.PhoneNumber,
                    FullName = $"{r.User.FirstName} {r.User.LastName}",
                    PersonalImage = r.ProfilePictureUrl,
                    
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task UpdateProfilePictureAsync(string userId, string profilePictureUrl)
        {
            Agent? agent = await FindAgentAsync(userId);

            this.guard.AgainstNull(agent, "No agent was found");

            agent!.ProfilePictureUrl = profilePictureUrl;

            await this.repo.SaveChangesAsync();
        }

        private async Task<Agent?> FindAgentAsync(string userId)
        {
            return await this.repo
                            .All<Agent>()
                            .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        private async Task<Renter?> FindRenterAsync(string userId)
        {
            return await this.repo
                            .All<Renter>()
                            .FirstOrDefaultAsync(a => a.UserId == userId);
        }
    }
}