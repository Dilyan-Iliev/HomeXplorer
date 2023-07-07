namespace HomeXplorer.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Renter;
    using HomeXplorer.Services.Exceptions.Contracts;
    using HomeXplorer.ViewModels.Property.Agent;
    using HomeXplorer.Data.Models.Entities;

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

        public async Task AddToFavoritesAsync(Guid propertyId, string userId)
        {
            Renter? renter = await RetrieveRenterAsync(userId);

            if (renter != null)
            {
                //If != null -> this property is already added to favorites
                RenterPropertyFavorite? favProperty = await this.repo
                    .All<RenterPropertyFavorite>()
                    .FirstOrDefaultAsync(rpf => rpf.RenterId == renter.Id
                        && rpf.PropertyId == propertyId);

                if (favProperty == null)
                {
                    favProperty = new RenterPropertyFavorite()
                    {
                        RenterId = renter.Id,
                        PropertyId = propertyId
                    };

                    renter.FavouriteProperties.Add(favProperty);

                    await this.repo.AddAsync<RenterPropertyFavorite>(favProperty);

                    await this.repo.SaveChangesAsync();
                }
            }
        }


        public async Task<IEnumerable<IndexSliderPropertyViewModel>> GetLastThreeAddedForSliderAsync()
        {
            var model = await this.repo
                .AllReadonly<Property>()
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
                    Price = p.Price
                })
                .Take(3)
                .ToListAsync();

            return model;
        }

        public async Task<IEnumerable<LatestPropertiesViewModel>> GetLastThreeAddedPropertiesAsync()
        {
            var model = await this.repo
                .AllReadonly<Property>()
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
                    //Visits
                })
                .Take(3)
                .ToListAsync();

            return model;
        }

        public async Task<IEnumerable<LatestPropertiesViewModel>> GetLastThreePropertiesNearbyAsync(string userId)
        {
            var renterCityId = await this.repo
                .AllReadonly<Renter>()
                .Where(r => r.UserId == userId)
                .Select(r => r.CityId)
                .FirstOrDefaultAsync();

            var model = await this.repo
                .AllReadonly<Property>()
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
                    //Visits
                })
                .Take(3)
                .ToListAsync();

            return model;
        }

        public async Task<RenterDetailsPropertyViewModel> GetPropertyDetailsAsync(Guid id, string userId)
        {
            var currentProperty = await this.repo
                .AllReadonly<Property>()
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
                    AgentProfilePicture = p.Agent.ProfilePictureUrl,
                    
                })
                .FirstOrDefaultAsync();

            if (currentProperty != null)
            {
                var renterId = await this.repo
                    .AllReadonly<Renter>()
                    .Where(r => r.UserId == userId)
                    .Select(r => r.Id)
                    .FirstOrDefaultAsync();

                bool isPropertyAddedToFav = await this.repo
                    .AllReadonly<RenterPropertyFavorite>()
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
                var rentedProperty = await this.repo
                    .GetByIdAsync<Property>(propertyId);

                renter.RentedProperties.Add(rentedProperty);

                rentedProperty.RenterId = renter.Id;

                await this.repo.SaveChangesAsync();
            }
        }

        private async Task<Renter?> RetrieveRenterAsync(string userId)
        {
            return await this.repo
                            .AllReadonly<Renter>()
                            .FirstOrDefaultAsync(r => r.UserId == userId);
        }
    }
}
