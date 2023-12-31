﻿namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.ViewModels.Property.Agent;
    using HomeXplorer.Services.Exceptions.Contracts;

    //TODO: add visits count

    public class AgentPropertyService
        : IAgentPropertyService
    {
        private readonly HomeXplorerDbContext dbContext;
        private readonly IGuard guard;
        private readonly IRepository repo;

        public AgentPropertyService(
            HomeXplorerDbContext dbContext,
            IGuard guard,
            IRepository repo)
        {
            this.dbContext = dbContext;
            this.guard = guard;
            this.repo = repo;
        }

        public async Task AddAsync(AddPropertyViewModel model, ICollection<string> imageUrls, string userId)
        {
            var currentAgent = await this.dbContext
                .Agents
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

            await this.dbContext.Properties.AddAsync(property);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AgentAllPropertiesViewModel> AllAsync(int pageNumber, int pageSize, PropertySorting propertySorting, string userId)
        {
            IQueryable<Property> properties = this.dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.Agent.UserId == userId && p.IsActive);

            properties = propertySorting switch
            {
                PropertySorting.Cheapest => properties.OrderBy(p => p.Price),
                PropertySorting.MostExpensive => properties.OrderByDescending(p => p.Price),
                PropertySorting.Oldest => properties.OrderBy(p => p.AddedOn),
                PropertySorting.Newest => properties.OrderByDescending(p => p.AddedOn),
                _ => properties
            };

            int totalProperties = await properties.CountAsync();
            int totalPages = (int)Math.Ceiling(totalProperties / (double)pageSize);
            var currentPage = pageNumber;

            if (totalPages > 0 && currentPage > totalPages)
            {
                currentPage = totalPages; // Adjust the current page if it exceeds the total pages
            }

            var propertiesForPage = properties
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            var mappedModel = await propertiesForPage
                .Select(p => new IndexAgentPropertiesViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Size = p.Size,
                    City = p.City.Name,
                    Status = p.PropertyStatus.Name,
                    CoverPhotoUrl = p.Images
                        .Where(i => i.PropertyId == p.Id)
                        .Select(i => i.Url)
                        .FirstOrDefault()!,
                    AddedOn = p.AddedOn.ToString("MM/dd/yyyy"),
                    Visits = this.dbContext
                        .PageVisits
                        .AsNoTracking()
                        .Where(pv => pv.Url.Contains(p.Id.ToString()))
                        .Select(pv => pv.VisitsCount)
                        .Count()

                })
                .ToListAsync();

            var model = new AgentAllPropertiesViewModel()
            {
                PropertySorting = propertySorting,
                Properties = mappedModel,
                PageNumber = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return model;
        }


        public async Task<bool> DeleteAsync(Guid id, string userId)
        {
            var property = await this.dbContext
                .Properties
                .Include(p => p.Agent)
                .ThenInclude(a => a.User)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            //if (property != null)
            //{
            //    property.IsActive = false;
            //    await this.dbContext.SaveChangesAsync();
            //}

            bool isDeletionSucceeded = false;

            if (property != null && property.Agent.UserId == userId)
            {
                property.IsActive = false;
                await this.dbContext.SaveChangesAsync();
                isDeletionSucceeded = true;
            }

            return isDeletionSucceeded;
        }

        public async Task EditAsync(EditPropertyViewModel model, Guid propertyId,
            ICollection<string>? imageUrls, ICollection<CloudImage> oldImages,
            ICollection<int>? deletedPhotosIds)
        {
            var property = await this.dbContext
                .Properties
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            property!.Name = model.Name;
            property.Description = model.Description;
            property.Price = model.Price;
            property.Size = model.Size;
            property.CityId = model.CityId;
            property.BuildingTypeId = model.BuildingTypeId;
            property.PropertyTypeId = model.PropertyTypeId;
            property.PropertyStatusId = model.PropertyStatusId;
            property.Images = oldImages;

            property.ModifiedOn = DateTime.UtcNow;

            if (imageUrls != null && imageUrls.Any())
            {
                foreach (var url in imageUrls)
                {
                    CloudImage cloudImage = new CloudImage()
                    {
                        Url = url,
                        PropertyId = property.Id
                    };

                    property.Images.Add(cloudImage);
                }
            }

            if (deletedPhotosIds?.Any() ?? false)
            {
                foreach (var photoId in deletedPhotosIds)
                {
                    var deletedImage = oldImages.
                        FirstOrDefault(i => i.Id == photoId);

                    if (deletedImage != null)
                    {
                        property.Images.Remove(deletedImage);
                        this.dbContext.CloudImages.Remove(deletedImage);
                    }
                }
            }

            this.dbContext.Properties.Update(property);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync<T>(object id) where T : class
        {
            T entity = await this.repo.GetByIdAsync<T>(id);
            return entity != null;
        }

        public Task<EditPropertyViewModel?> FindByIdAsync(Guid propertyId)
        {
            return this.dbContext
                .Properties
                .Where(p => p.Id == propertyId)
                .Select(p => new EditPropertyViewModel()
                {
                    AgentId = p.Agent.UserId,
                    Name = p.Name,
                    Description = p.Description,
                    Address = p.Address,
                    Price = p.Price,
                    Size = p.Size,
                    CountryId = p.City.CountryId,
                    CityId = p.CityId,
                    BuildingTypeId = p.BuildingTypeId,
                    PropertyTypeId = p.PropertyTypeId,
                    PropertyStatusId = p.PropertyStatusId,

                    AddedImages = p.Images
                        .Select(i => new PropertyImagesViewModel()
                        {
                            Id = i.Id,
                            Url = i.Url
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PropertyImagesViewModel>> GetAllImageUrlsForPropertyAsync(Guid propertyId)
        {
            return await this.dbContext
                .CloudImages
                .AsNoTracking()
                .Where(ci => ci.PropertyId == propertyId)
                .Select(ci => new PropertyImagesViewModel()
                {
                    Id = ci.Id,
                    Url = ci.Url
                })
                .ToListAsync();
        }

        public async Task<DetailsPropertyViewModel?> GetDetailsAsync(Guid id)
        {
            var currentProperty = await this.dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new DetailsPropertyViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Size = p.Size,
                    Address = p.Address,
                    City = p.City.Name,
                    Country = p.City.Country.Name,
                    AddedOd = p.AddedOn.ToString("MM/dd/yyyy"),
                    PropertyStatus = p.PropertyStatus.Name,
                    PropertyType = p.PropertyType.Name,
                    BuildingType = p.BuildingType.Name,
                    AgentId = p.Agent.UserId,
                    Images = p.Images
                        .Select(i => new PropertyImagesViewModel()
                        {
                            Id = i.Id,
                            Url = i.Url
                        })
                        .ToList(),
                })
                .FirstOrDefaultAsync();

            if (currentProperty != null)
            {
                return currentProperty;
            }

            return null;
        }

        public async Task<IEnumerable<IndexAgentPropertiesViewModel>> GetLastThreeAsync(string userId)
        {
            var currentAgent = await this.dbContext
                .Agents
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.UserId == userId);

            this.guard.AgainstNull(currentAgent, "Invalid agent");

            var lastThreeProperties = await this.dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.AgentId == currentAgent!.Id)
                .Where(p => p.IsActive)
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
                    Visits = this.dbContext
                        .PageVisits
                        .AsNoTracking()
                        .Where(pv => pv.Url.Contains(p.Id.ToString()))
                        .Select(pv => pv.VisitsCount)
                        .Count()
                })
                .Take(3)
                .ToListAsync();

            //var propertyIds = lastThreeProperties.Select(p => p.Id).ToList();
            //var propertyUrls = propertyIds.ToDictionary(id => id, GetPropertyUrl);

            //foreach (var property in lastThreeProperties)
            //{
            //    property.Visits = this.repo.All<PageVisit>()
            //        .AsEnumerable()
            //        .Count(pv => pv.Url == propertyUrls[property.Id]);
            //}

            return lastThreeProperties;
        }

        //private string GetPropertyUrl(Guid propertyId)
        //{
        //    // Logic to get the property URL based on the property ID
        //    // Adjust this based on your URL structure or how you retrieve the property URL
        //    // Example: if the URL format is "/property/{propertyId}", return $"/property/{propertyId}"
        //    return $"/property/{propertyId}";
        //}
    }
}
