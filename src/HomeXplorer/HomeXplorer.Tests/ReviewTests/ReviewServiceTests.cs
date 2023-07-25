namespace HomeXplorer.Tests.ReviewTests
{
    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;
    using HomeXplorer.ViewModels.Property.Renter;

    public class ReviewServiceTests
        : BaseTestsOptions
    {
        private IReviewService rs;

        [SetUp]
        public void Initial()
        {
            rs = new ReviewService(dbContext);
        }

        [Test]
        public void Review_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
            Assert.That(rs, Is.Not.Null);
            Assert.That(rs, Is.TypeOf<ReviewService>());
            Assert.That(rs, Is.InstanceOf<IReviewService>());
        }

        [Test]
        public async Task Add_Method_Should_Add_New_Review_If_Renter_Is_Not_Null()
        {
            // Arrange
            ApplicationUser user = new()
            {
                Id = "6fac668d-57b2-47e1-9cc6-e4ad441e5aa3",
                FirstName = "Test",
                LastName = "Test2"
            };
            Renter renter = new() { Id = 1, User = user, ProfilePictureUrl = "test" };

            await dbContext.Users.AddAsync(user);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.SaveChangesAsync();

            var reviewModel = new AddReviewViewModel() { FullName = user.FirstName + " " + user.LastName, Description = "Test Description" };

            // Act
            await rs.AddAsync(reviewModel, user.Id);

            // Assert
            Assert.That(await dbContext.Reviews.CountAsync(), Is.GreaterThan(0));
            Assert.That(await dbContext.Reviews.CountAsync(), Is.EqualTo(1));
        }

        [Test]
        public async Task Get_All_Reviews_Method_Should_Return_Correct_Data_When_Reviews_Are_Approved()
        {
            // Arrange
            ApplicationUser user = new()
            {
                Id = "6fac668d-57b2-47e1-9cc6-e4ad441e5aa3",
                FirstName = "Test",
                LastName = "Test2"
            };
            Renter renter = new() { Id = 1, User = user, ProfilePictureUrl = "test" };

            var reviews = new List<Review>()
            {
                new Review() {Id = 1, Description = "Test1", ReviewCreatorId = renter.Id, IsApproved = true},
                new Review() {Id = 3, Description = "Test3", ReviewCreatorId = renter.Id, IsApproved = true},
                new Review() {Id = 2, Description = "Test2", ReviewCreatorId = renter.Id},
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.Reviews.AddRangeAsync(reviews);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await rs.GetAllReviewsAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task Get_All_Reviews_Method_Should_Return_Correct_Data_When_Reviews_Are_Not_Approved()
        {
            // Arrange
            ApplicationUser user = new()
            {
                Id = "6fac668d-57b2-47e1-9cc6-e4ad441e5aa3",
                FirstName = "Test",
                LastName = "Test2"
            };
            Renter renter = new() { Id = 1, User = user, ProfilePictureUrl = "test" };

            var reviews = new List<Review>()
            {
                new Review() {Id = 1, Description = "Test1", ReviewCreatorId = renter.Id},
                new Review() {Id = 3, Description = "Test3", ReviewCreatorId = renter.Id},
                new Review() {Id = 2, Description = "Test2", ReviewCreatorId = renter.Id},
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.Reviews.AddRangeAsync(reviews);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await rs.GetAllReviewsAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}
