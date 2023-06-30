namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Agent;
    using HomeXplorer.Services.Exceptions.Contracts;

    public class PropertyService
        : IPropertyService
    {
        private readonly IRepository repo;
        private readonly IGuard guard;

        public PropertyService(
            IRepository repo, IGuard guard)
        {
            this.repo = repo;
            this.guard = guard;
        }

        public async Task AddAsync(AddPropertyViewModel model, ICollection<string> imageUrls, string userId)
        {
            var currentAgent = await this.repo
                .All<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId);

            Property property = new Property()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Size = model.Size,
                Address = model.Address,
                CityId = model.CityId,
                PropertyTypeId = model.PropertyTypeId,
                PropertyStatusId = model.PropertyStatusId,
                BuildingTypeId = model.BuildingTypeId,
                AgentId = currentAgent!.Id,
                PetsAllowed = false,
                AddedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            foreach (var imageUrl in imageUrls)
            {
                CloudImage cloudImage = new CloudImage()
                {
                    PropertyId = property.Id,
                    Url = imageUrl
                };

                property.Images.Add(cloudImage);
            }

            await this.repo.AddAsync<Property>(property);
            await this.repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndexAgentPropertiesViewModel>> GetLastThreeAsync(string userId)
        {
            var currentAgent = await this.repo
                .AllReadonly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId);

            this.guard.AgainstNull(currentAgent, "Invalid agent");

            var lastThreeProperties = await this.repo
                .AllReadonly<Property>()
                .Where(p => p.AgentId == currentAgent.Id)
                .OrderByDescending(p => p.AddedOn)
                .Select(p => new IndexAgentPropertiesViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Size = p.Size,
                    AddedOn = p.AddedOn.ToString("MM/dd/yyyy"),
                    Status = p.PropertyStatus.Name,
                    CoverPhotoUrl = p.Images
                        .Where(i => i.PropertyId == p.Id)
                        .Select(i => i.Url)
                        .FirstOrDefault()!,
                    City = p.City.Name,
                })
                .Take(3)
                .ToListAsync();

            var propertyIds = lastThreeProperties.Select(p => p.Id).ToList();
            var propertyUrls = propertyIds.ToDictionary(id => id, GetPropertyUrl);

            foreach (var property in lastThreeProperties)
            {
                property.Visits = this.repo.All<PageVisit>()
                    .AsEnumerable()
                    .Count(pv => pv.Url == propertyUrls[property.Id]);
            }

            return lastThreeProperties;
        }

        private string GetPropertyUrl(Guid propertyId)
        {
            // Logic to get the property URL based on the property ID
            // Adjust this based on your URL structure or how you retrieve the property URL
            // Example: if the URL format is "/property/{propertyId}", return $"/property/{propertyId}"
            return $"/property/{propertyId}";
        }
    }
}
