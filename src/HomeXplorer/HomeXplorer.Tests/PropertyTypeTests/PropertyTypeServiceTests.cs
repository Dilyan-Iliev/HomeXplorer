﻿namespace HomeXplorer.Tests.PropertyTypeTests
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;

    public class PropertyTypeServiceTests
        : BaseTestsOptions
    {
        private IPropertyTypeService pts;

        [SetUp] // Use NUnit.Framework.SetUp attribute
        public void TestSetUp()
        {
            pts = new PropertyTypeService(dbContext);
        }

        [Test]
        public void Property_Type_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
            //Act & Assert
            Assert.That(pts, Is.Not.Null);
            Assert.That(pts, Is.TypeOf<PropertyTypeService>());
            Assert.That(pts, Is.InstanceOf<IPropertyTypeService>());
        }

        [Test]
        public async Task Get_Property_Types_Method_Should_Return_Correct_Data()
        {
            //Arrange
            var propertyTypes = new[]
            {
                new PropertyType() {Id = 1, Name = "Test1"},
                new PropertyType() {Id = 2, Name = "Test2"},
            };

            await dbContext.AddRangeAsync(propertyTypes);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await pts.GetPropertyTypesAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }
    }
}
