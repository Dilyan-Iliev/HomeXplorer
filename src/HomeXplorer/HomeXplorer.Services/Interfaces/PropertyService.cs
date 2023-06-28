namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property;

    public class PropertyService
        : IPropertyService
    {
        private readonly IRepository repo;

        public PropertyService(
            IRepository repo)
        {
            this.repo = repo;
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
                PetsAllowed = false
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
    }
}
