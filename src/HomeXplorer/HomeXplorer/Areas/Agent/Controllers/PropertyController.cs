namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using CloudinaryDotNet;

    using HomeXplorer.Common;
    using HomeXplorer.Extensions;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Exceptions;
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.ViewModels.Property.Agent;

    using static HomeXplorer.Common.UserRoleConstants;

    public class PropertyController : BaseAgentController
    {
        private readonly IPropertyTypeService propertyTypeService;
        private readonly ICountryService countryService;
        private readonly IBuildingTypeService buildingTypeService;
        private readonly Cloudinary cloudinary;
        private readonly IAgentPropertyService propertyService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IPropertyStatusService propertyStatusService;
        private readonly IRepository repo;

        public PropertyController(
            IPropertyTypeService propertyTypeService,
            ICountryService countryService,
            IBuildingTypeService buildingTypeService,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary,
            IAgentPropertyService propertyService,
            IPropertyStatusService propertyStatusService,
            IRepository repo)
        {
            this.propertyTypeService = propertyTypeService;
            this.countryService = countryService;
            this.buildingTypeService = buildingTypeService;
            this.cloudinary = cloudinary;
            this.propertyService = propertyService;
            this.cloudinaryService = cloudinaryService;
            this.propertyStatusService = propertyStatusService;
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> All(int pageNumber = 1, int pageSize = 3,
            PropertySorting propertySorting = PropertySorting.Default)
        {
            string userId = this.User.GetId();

            try
            {
                var model = await this.propertyService.AllAsync(pageNumber, pageSize, propertySorting, userId);
                return View(model);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var model = new AddPropertyViewModel()
                {
                    Countries = await this.countryService.GetCountriesAsync(),
                    PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync(),
                    BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync()
                };

                return this.View(model);

            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPropertyViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                try
                {
                    model.Countries = await this.countryService.GetCountriesAsync();
                    model.PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync();
                    model.BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync();

                    return this.View(model);

                }
                catch (Exception)
                {
                    return this.TempDataView();
                }
            }

            try
            {
                //Check if some of the selected options from dropdowns are valid
                bool selectedCountryIdExists = await this.propertyService.ExistByIdAsync<Country>(model.CountryId);
                bool selectedCityIdExists = await this.propertyService.ExistByIdAsync<City>(model.CityId);
                bool selectedPropertyTypeIdExists = await this.propertyService.ExistByIdAsync<PropertyType>(model.PropertyTypeId);
                bool selectedBuildingTypeIdExists = await this.propertyService.ExistByIdAsync<BuildingType>(model.BuildingTypeId);

                if (!selectedCountryIdExists || !selectedCityIdExists
                    || !selectedPropertyTypeIdExists || !selectedBuildingTypeIdExists)
                {
                    this.TempData["InvalidDropdownOption"] = "You must choose a valid option from the dropdowns";
                    model.Countries = await this.countryService.GetCountriesAsync();
                    model.PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync();
                    model.BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync();
                    return this.View(model);
                }

                var modelImages = model.Images;
                var imagesUrls = await this.cloudinaryService.UploadMany(this.cloudinary, modelImages);

                string userId = this.User.GetId();

                await this.propertyService.AddAsync(model, imagesUrls, userId);

                return this.RedirectToAction("Index", "Home", new { area = UserRoleConstants.Agent });
            }
            catch (InvalidFileExtensionException ife)
            {
                model?.Errors?.Add(ife.Message);

                model!.Countries = await this.countryService.GetCountriesAsync();
                model.PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync();
                model.BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync();

                return this.View(model);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var property = await this.propertyService
                    .GetDetailsAsync(id);


                if (property == null)
                {
                    this.TempData["DetailsError"] = "Can not show the details of the property";

                    return this.RedirectToAction("Index", "Home", new { area = UserRoleConstants.Agent });
                }

                //check if the agent is the uploader of the property in order to see its details:
                string currentUserId = this.User.GetId();

                string? agentUploaderId = property?.AgentId;

                if (currentUserId != agentUploaderId)
                {
                    this.TempData["BadAccess"] = "You are not uploader of this property";
                    return this.RedirectToAction("Index", "Home", new { area = UserRoleConstants.Agent});
                }

                return this.View(property);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await this.propertyService.DeleteAsync(id, this.User.GetId());

                this.TempData["SuccessDelete"] = "You removed successfuly a property";

                return this.RedirectToAction("Index", "Home", new { area = UserRoleConstants.Agent });
            }
            catch (Exception)
            {
                this.TempData["FailedDelete"] = "Something went wrong, try again";
                return this.RedirectToAction("Details", "Home", new { area = UserRoleConstants.Agent, id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                EditPropertyViewModel? model = await this.propertyService
                    .FindByIdAsync(id);

                if (model != null)
                {
                    var currentUserId = this.User.GetId();
                    var agentUploaderId = model.AgentId;

                    if (currentUserId != agentUploaderId)
                    {
                        this.TempData["BadAccess"] = "You are not uploader of this property";
                        return this.RedirectToAction("Index", "Home", new { area = UserRoleConstants.Agent });
                    }

                    var editModel = new EditPropertyViewModel()
                    {
                        Name = model.Name,
                        Address = model.Address,
                        Price = model.Price,
                        Size = model.Size,
                        Description = model.Description,
                        BuildingTypeId = model.BuildingTypeId,
                        CountryId = model.CountryId,
                        CityId = model.CityId,
                        PropertyTypeId = model.PropertyTypeId,
                        PropertyStatusId = model.PropertyStatusId,
                        AgentId = model.AgentId,

                        AddedImages = await this.propertyService.GetAllImageUrlsForPropertyAsync(id),

                        BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync(),
                        Countries = await this.countryService.GetCountriesAsync(),
                        PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync(),
                        PropertyStatuses = await this.propertyStatusService.GetPropertyStatusesAsync()
                    };

                    return this.View(editModel);
                }
                return this.View();
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPropertyViewModel model, Guid id)
        {
            if (!this.ModelState.IsValid)
            {
                try
                {
                    model.BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync();
                    model.Countries = await this.countryService.GetCountriesAsync();
                    model.PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync();
                    model.PropertyStatuses = await this.propertyStatusService.GetPropertyStatusesAsync();

                    return this.View(model);
                }
                catch (Exception)
                {
                    return this.TempDataView();
                }
            }

            try
            {
                bool selectedCountryIdExists = await this.propertyService.ExistByIdAsync<Country>(model.CountryId);
                bool selectedCityIdExists = await this.propertyService.ExistByIdAsync<City>(model.CityId);
                bool selectedPropertyTypeIdExists = await this.propertyService.ExistByIdAsync<PropertyType>(model.PropertyTypeId);
                bool selectedBuildingTypeIdExists = await this.propertyService.ExistByIdAsync<BuildingType>(model.BuildingTypeId);

                if (!selectedCountryIdExists || !selectedCityIdExists
                    || !selectedPropertyTypeIdExists || !selectedBuildingTypeIdExists)
                {
                    this.TempData["InvalidDropdownOption"] = "You must choose a valid option from the dropdowns";
                    model.Countries = await this.countryService.GetCountriesAsync();
                    model.PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync();
                    model.BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync();
                    return this.View(model);
                }

                var newImages = model.NewImages;

                var oldPropertyImages = await this.repo
                    .AllReadonly<CloudImage>()
                    .Where(p => p.PropertyId == id)
                    .ToListAsync();

                ICollection<string>? imagesUrls = null;

                if (newImages!.Any())
                {
                    imagesUrls = await this.cloudinaryService.UploadMany(this.cloudinary, newImages!);
                }

                var deletedPhotos = model.DeletedPhotosIds;

                await this.propertyService.EditAsync(model, id, imagesUrls, oldPropertyImages, deletedPhotos!);

                return this.RedirectToAction("Details", "Property", new { area = UserRoleConstants.Agent, id });
            }
            catch (InvalidFileExtensionException ife)
            {
                model?.Errors?.Add(ife.Message);

                model!.Countries = await this.countryService.GetCountriesAsync();
                model.PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync();
                model.BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync();

                return this.View(model);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        private IActionResult TempDataView()
        {
            this.TempData["UnexpectedError"] = "Something went wrong, please try again";
            return this.View();
        }
    }
}