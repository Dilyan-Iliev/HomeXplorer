namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Renter;

    public class HomePropertyService
        : IHomePropertyService
    {
        private readonly HomeXplorerDbContext dbContext;

        public HomePropertyService(HomeXplorerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<LatestPropertiesViewModel>> GetAllPropertiesAsync()
        {
            return await this.dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.AddedOn)
                .Select(p => new LatestPropertiesViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    City = p.City.Name,
                    Size = p.Size,
                    Price = p.Price,
                    Status = p.PropertyStatus.Name,
                    AddedOn = p.AddedOn.ToString("MM/dd/yyyy"),
                    CoverImageUrl = p.Images
                            .Where(i => i.PropertyId == p.Id)
                            .Select(i => i.Url)
                            .FirstOrDefault()!,
                    Visits = this.dbContext
                        .PageVisits
                        .AsNoTracking()
                        .Where(pv => pv.Url.Contains(p.Id.ToString()))
                        .Select(pv => pv.VisitsCount)
                        .Count()
                })
                .ToListAsync();
        }
    }
}
