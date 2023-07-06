namespace HomeXplorer.Services.Interfaces
{
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Exceptions.Contracts;
    using HomeXplorer.ViewModels.Property.Renter;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RenterPropertyService
        : IRenterPropertyService
    {
        private readonly IGuard guard;
        private readonly IRepository repo;

        public RenterPropertyService(IGuard guard,
            IRepository repo)
        {
            this.guard = guard;
            this.repo = repo;
        }

        public async Task<IEnumerable<IndexSliderPropertyViewModel>> GetLastThreeAddedForSliderAsync()
        {
            return await this.repo
                .AllReadonly<Property>()
                .Select(p => new IndexSliderPropertyViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    City = p.City.Name,
                    Country = p.City.Country.Name,
                    Address = p.Address,
                    CoverImageUrl = p.Images
                        .Where(i => i.PropertyId == p.Id)
                        .Select(i => i.Url)
                        .FirstOrDefault()!,
                    Price = p.Price
                })
                .ToListAsync();
        }
    }
}
