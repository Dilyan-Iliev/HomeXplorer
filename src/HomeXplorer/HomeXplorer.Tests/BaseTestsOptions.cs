namespace HomeXplorer.Tests
{
    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Core.Contexts;

    public class BaseTestsOptions
    {
        protected DbContextOptions<HomeXplorerDbContext> options;
        protected HomeXplorerDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<HomeXplorerDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase")
                .Options;

            dbContext = new HomeXplorerDbContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            //dbContext.Dispose();
        }
    }
}
