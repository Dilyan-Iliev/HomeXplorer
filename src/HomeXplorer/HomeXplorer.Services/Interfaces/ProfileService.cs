namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Profile;
    using HomeXplorer.ViewModels.Property.Agent;
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

        public async Task<AgentProfileViewModel> GetAgentProfileInfo(string userId)
        {
            Agent? agent = await this.repo
                .AllReadonly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId);

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
                        .SelectMany(p => p.Images)
                            .Select(i => new PropertyImagesViewModel()
                            {
                                Id = i.Id,
                                Url = i.Url,
                            })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task UpdateProfilePicture(string userId, string profilePictureUrl)
        {
            var agent = await this.repo
                .All<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId);

            agent.ProfilePictureUrl = profilePictureUrl;

            await this.repo.SaveChangesAsync();
        }
    }
}
