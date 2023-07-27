namespace HomeXplorer.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Data.Models.Entities;
    using HomeXplorer.ViewModels.Property.Agent;
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.ViewModels.Property.Renter;
    using HomeXplorer.Services.Exceptions.Contracts;
    using HomeXplorer.Core.Contexts;

    public class RenterPropertyService
        : IRenterPropertyService
    {
        private readonly IGuard guard;
        private readonly HomeXplorerDbContext dbContext;

        public RenterPropertyService(IGuard guard,
            HomeXplorerDbContext dbContext)
        {
            this.guard = guard;
            this.dbContext = dbContext;
        }

        public async Task AddToFavoritesAsync(Guid propertyId, string userId)
        {
            Renter? renter = await RetrieveRenterAsync(userId);

            if (renter != null)
            {
                //If != null -> this property is already added to favorites
                RenterPropertyFavorite? favProperty = await this.dbContext
                    .RentersPropertiesFavorites
                    .FirstOrDefaultAsync(rpf => rpf.RenterId == renter.Id
                        && rpf.PropertyId == propertyId);

                if (favProperty == null)
                {
                    favProperty = new RenterPropertyFavorite()
                    {
                        RenterId = renter.Id,
                        PropertyId = propertyId,
                        AddedOn = DateTime.UtcNow
                    };

                    renter.FavouriteProperties!.Add(favProperty);

                    await this.dbContext.RentersPropertiesFavorites.AddAsync(favProperty);

                    await this.dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveFromFavoritesAsync(Guid propertyId, string userId)
        {
            Renter? renter = await RetrieveRenterAsync(userId);

            if (renter != null)
            {
                RenterPropertyFavorite? favProperty = await this.dbContext
                    .RentersPropertiesFavorites
                    .FirstOrDefaultAsync(rpf => rpf.RenterId == renter.Id
                        && rpf.PropertyId == propertyId);

                if (favProperty != null)
                {
                    renter.FavouriteProperties!.Remove(favProperty);

                    this.dbContext.RentersPropertiesFavorites.Remove(favProperty);
                    await this.dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<RenterAllPropertiesViewModel> AllAsync(int pageNumber, int pageSize, PropertySorting propertySorting)
        {
            IQueryable<Property> propertiesQuery = this.dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.RenterId == null && p.IsActive);

            propertiesQuery = propertySorting switch
            {
                PropertySorting.Cheapest => propertiesQuery.OrderBy(p => p.Price),
                PropertySorting.MostExpensive => propertiesQuery.OrderByDescending(p => p.Price),
                PropertySorting.Oldest => propertiesQuery.OrderBy(p => p.AddedOn),
                PropertySorting.Newest => propertiesQuery.OrderByDescending(p => p.AddedOn),
                _ => propertiesQuery
            };

            int totalProperties = await propertiesQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalProperties / (double)pageSize);
            var currentPage = pageNumber;

            if (totalPages > 0 && currentPage > totalPages)
            {
                currentPage = totalPages; // Adjust the current page if it exceeds the total pages
            }

            var propertiesForPage = propertiesQuery
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            var mappedModel = await propertiesForPage
                .Select(p => new LatestPropertiesViewModel()
                {
                    Id = p.Id,
                    City = p.City.Name,
                    Name = p.Name,
                    Price = p.Price,
                    Size = p.Size,
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

            var returnedModel = new RenterAllPropertiesViewModel()
            {
                Properties = mappedModel,
                PropertySorting = propertySorting,
                PageNumber = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return returnedModel;
        }

        public async Task<RenterAllPropertiesViewModel> AllNearbyAsync(int pageNumber, int pageSize,
            PropertySorting propertySorting, string userId)
        {
            Renter? renter = await this.RetrieveRenterAsync(userId);

            IQueryable<Property> propertiesQuery = this.dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.RenterId == null && p.IsActive)
                .Where(p => p.CityId == renter!.CityId);

            propertiesQuery = propertySorting switch
            {
                PropertySorting.Cheapest => propertiesQuery.OrderBy(p => p.Price),
                PropertySorting.MostExpensive => propertiesQuery.OrderByDescending(p => p.Price),
                PropertySorting.Oldest => propertiesQuery.OrderBy(p => p.AddedOn),
                PropertySorting.Newest => propertiesQuery.OrderByDescending(p => p.AddedOn),
                _ => propertiesQuery
            };

            int totalProperties = await propertiesQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalProperties / (double)pageSize);
            var currentPage = pageNumber;

            if (totalPages > 0 && currentPage > totalPages)
            {
                currentPage = totalPages; // Adjust the current page if it exceeds the total pages
            }

            var propertiesForPage = propertiesQuery
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            var mappedModel = await propertiesForPage
                .Select(p => new LatestPropertiesViewModel()
                {
                    Id = p.Id,
                    City = p.City.Name,
                    Name = p.Name,
                    Price = p.Price,
                    Size = p.Size,
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

            var returnedModel = new RenterAllPropertiesViewModel()
            {
                Properties = mappedModel,
                PropertySorting = propertySorting,
                PageNumber = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return returnedModel;
        }

        public async Task<IEnumerable<LatestPropertiesViewModel>> GetAllFavoritesAsync(string userId)
        {
            Renter? renter = await this.RetrieveRenterAsync(userId);

            if (renter != null)
            {
                var model = await this.dbContext
                    .RentersPropertiesFavorites
                    .AsNoTracking()
                    .Where(p => p.RenterId == renter.Id && p.Property!.IsActive)
                    .OrderByDescending(p => p.AddedOn)
                    .Select(p => new LatestPropertiesViewModel()
                    {
                        Id = p.Property!.Id,
                        Name = p.Property.Name,
                        City = p.Property.City.Name,
                        Size = p.Property.Size,
                        Price = p.Property.Price,
                        Status = p.Property.PropertyStatus.Name,
                        AddedOn = p.Property.AddedOn.ToString("MM/dd/yyyy"),
                        CoverImageUrl = p.Property.Images
                            .Where(i => i.PropertyId == p.PropertyId)
                            .Select(i => i.Url)
                            .FirstOrDefault()!,
                        Visits = this.dbContext
                        .PageVisits
                        .AsNoTracking()
                        .Where(pv => pv.Url.Contains(p.Property.Id.ToString()))
                        .Select(pv => pv.VisitsCount)
                        .Count()
                    })
                    .ToListAsync();

                return model;
            }

            return null!;
        }

        public async Task<IEnumerable<LatestPropertiesViewModel>> GetAllRentedAsync(string userId)
        {
            Renter? renter = await this.RetrieveRenterAsync(userId);

            if (renter == null)
            {
                return null!;
            }

            return await this.dbContext
                .Properties
                .AsNoTracking()
                .Where(rp => rp.RenterId == renter.Id)
                .Select(p => new LatestPropertiesViewModel()
                {
                    Id = p!.Id,
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

        public async Task<IEnumerable<IndexSliderPropertyViewModel>> GetLastThreeAddedForSliderAsync()
        {
            var model = await this.dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.IsActive && p.RenterId == null)
                .OrderByDescending(p => p.AddedOn)
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
                    Price = p.Price,
                })
                .Take(3)
                .ToListAsync();

            return model;
        }

        public async Task<IEnumerable<LatestPropertiesViewModel>> GetLastThreeAddedPropertiesAsync()
        {
            var model = await this.dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.IsActive && p.RenterId == null)
                .Select(p => new LatestPropertiesViewModel()
                {
                    Id = p.Id,
                    City = p.City.Name,
                    Name = p.Name,
                    Price = p.Price,
                    Size = p.Size,
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
                .Take(3)
                .ToListAsync();

            return model;
        }

        public async Task<IEnumerable<LatestPropertiesViewModel>> GetLastThreePropertiesNearbyAsync(string userId)
        {
            var renterCityId = await this.dbContext
                .Renters
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .Select(r => r.CityId)
                .FirstOrDefaultAsync();

            var model = await this.dbContext
                .Properties
                .AsNoTracking()
                .OrderByDescending(p => p.AddedOn)
                .Where(p => p.CityId == renterCityId)
                .Where(p => p.IsActive && p.RenterId == null)
                .Select(p => new LatestPropertiesViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    City = p.City.Name,
                    Price = p.Price,
                    Size = p.Size,
                    Status = p.PropertyStatus.Name,
                    AddedOn = p.AddedOn.ToString("MM/dd/yyyy"),
                    CoverImageUrl = p.Images
                        .Where(i => i.PropertyId == p.Id)
                        .Select(i => i.Url).
                        FirstOrDefault()!,
                    Visits = this.dbContext
                        .PageVisits
                        .AsNoTracking()
                        .Where(pv => pv.Url.Contains(p.Id.ToString()))
                        .Select(pv => pv.VisitsCount)
                        .Count()
                })
                .Take(3)
                .ToListAsync();

            return model;
        }

        public async Task<RenterDetailsPropertyViewModel> GetPropertyDetailsAsync(Guid id, string? userId = null)
        {
            var currentProperty = await this.dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new RenterDetailsPropertyViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Address = p.Address,
                    City = p.City.Name,
                    Country = p.City.Country.Name,
                    Size = p.Size,
                    Price = p.Price,
                    AddedOd = p.AddedOn.ToString("MM/dd/yyyy"),
                    PropertyType = p.PropertyType.Name,
                    PropertyStatus = p.PropertyStatus.Name,
                    BuildingType = p.BuildingType.Name,
                    IsRented = p.RenterId != null,
                    Images = p.Images
                        .Select(i => new PropertyImagesViewModel()
                        {
                            Id = i.Id,
                            Url = i.Url
                        })
                        .ToList(),
                    AgentEmail = p.Agent.User.Email,
                    AgentPhone = p.Agent.User.PhoneNumber,
                    AgentFullName = $"{p.Agent.User.FirstName} {p.Agent.User.LastName}",
                    AgentProfilePicture = p.Agent.ProfilePictureUrl
                })
                .FirstOrDefaultAsync();

            if (currentProperty != null)
            {
                var renterId = await this.dbContext
                    .Renters
                    .AsNoTracking()
                    .Where(r => r.UserId == userId)
                    .Select(r => r.Id)
                    .FirstOrDefaultAsync();

                bool isPropertyAddedToFav = await this.dbContext
                    .RentersPropertiesFavorites
                    .AsNoTracking()
                    .AnyAsync(rpf => rpf.PropertyId == id && rpf.RenterId == renterId);

                currentProperty.IsAddedToFavs = isPropertyAddedToFav;

                return currentProperty;
            }

            return null!;
        }
        
        public async Task RentAsync(Guid propertyId, string userId)
        {
            Renter? renter = await RetrieveRenterAsync(userId);

            if (renter != null)
            {
                var rentedProperty = await this.dbContext
                    .Properties
                    .Include(p => p.PropertyStatus) // Include PropertyStatus entity
                    .FirstOrDefaultAsync(p => p.Id == propertyId);

                //renter.RentedProperties!.Add(rentedProperty!);

                rentedProperty!.PropertyStatus.Name = "Taken";
                rentedProperty.RenterId = renter.Id;

                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task LeaveAsync(Guid propertyId, string userId)
        {
            Renter? renter = await RetrieveRenterAsync(userId);

            if (renter != null)
            {
                var rentedProperty = await this.dbContext
                    .Properties
                    .Include(p => p.PropertyStatus) // Include PropertyStatus entity
                    .FirstOrDefaultAsync(p => p.Id == propertyId);

                //renter.RentedProperties!.Remove(rentedProperty);

                rentedProperty!.RenterId = null;
                rentedProperty.PropertyStatus.Name = "Free";

                await this.dbContext.SaveChangesAsync();
            }
        }

        private async Task<Renter?> RetrieveRenterAsync(string userId)
        {
            return await this.dbContext
                            .Renters
                            .AsNoTracking()
                            .FirstOrDefaultAsync(r => r.UserId == userId);
        }
    }
}
