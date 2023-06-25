namespace HomeXplorer.Services.Interfaces
{
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property;
    using System.Threading.Tasks;

    public class PropertyService
        : IPropertyService
    {
        private readonly IRepository repo;

        public PropertyService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task AddAsync(AddPropertyViewModel model)
        {
            Property property = new Property();

            //TODO: mapping

            await this.repo.AddAsync<Property>(property);
            await this.repo.SaveChangesAsync();
        }
    }
}
