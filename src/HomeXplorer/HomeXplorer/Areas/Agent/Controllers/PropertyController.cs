namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CloudinaryDotNet;

    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property;

    public class PropertyController : BaseAgentController
    {
        private readonly IPropertyTypeService propertyTypeService;
        private readonly ICountryService countryService;
        private readonly IBuildingTypeService buildingTypeService;
        private readonly Cloudinary cloudinary;
        private readonly IPropertyService propertyService;
        private readonly ICloudinaryService cloudinaryService;

        public PropertyController(
            IPropertyTypeService propertyTypeService,
            ICountryService countryService,
            IBuildingTypeService buildingTypeService,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary,
            IPropertyService propertyService)
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

            var modelImages = model.Images;
            var imagesUrls = await this.cloudinaryService.UploadMany(this.cloudinary, modelImages);

            string userId = this.User.GetUserId();

            await this.propertyService.AddAsync(model, imagesUrls, userId);

            return this.Ok();
        }
    }
}