namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CloudinaryDotNet;

    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Exceptions;
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

        public PropertyController(
            IPropertyTypeService propertyTypeService,
            ICountryService countryService,
            IBuildingTypeService buildingTypeService,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary,
            IAgentPropertyService propertyService)
        {
            this.propertyTypeService = propertyTypeService;
            this.countryService = countryService;
            this.buildingTypeService = buildingTypeService;
            this.cloudinary = cloudinary;
            this.propertyService = propertyService;
            this.cloudinaryService = cloudinaryService;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Check if renter user can access this view and others

            var model = new AddPropertyViewModel()
            {
                Countries = await this.countryService.GetCountriesAsync(),
                PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync(),
                BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync()
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPropertyViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Countries = await this.countryService.GetCountriesAsync();
                model.PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync();
                model.BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync();

                return this.View(model);
            }

            try
            {
                var modelImages = model.Images;
                var imagesUrls = await this.cloudinaryService.UploadMany(this.cloudinary, modelImages);

                string userId = this.User.GetId();

                //TODO: check if some of the dropdowns are valid : (existsById)



                await this.propertyService.AddAsync(model, imagesUrls, userId);

                return this.RedirectToAction("Index", "Home", new { area = Agent });
            }
            catch (InvalidFileExtensionException ife)
            {
                model?.Errors?.Add(ife.Message);

                model.Countries = await this.countryService.GetCountriesAsync();
                model.PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync();
                model.BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync();

                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var property = await this.propertyService
                .GetDetailsAsync(id);

            if (property == null)
            {
                this.TempData["DetailsError"] = "Can not show the details of the property";

                return this.RedirectToAction("Index", "Home", new { area = Agent });
            }

            return this.View(property);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await this.propertyService.DeleteAsync(id);

                this.TempData["SuccessDelete"] = "You removed successfuly a property";

                return this.RedirectToAction("Index", "Home", new { area = Agent });
            }
            catch (Exception)
            {
                this.TempData["FailedDelete"] = "Something went wrong, try again";
                return this.RedirectToAction("Details", "Home", new { area = Agent, id });
            }

        }
    }
}