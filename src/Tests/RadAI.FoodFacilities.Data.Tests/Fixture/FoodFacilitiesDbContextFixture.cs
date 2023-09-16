using Microsoft.EntityFrameworkCore;

namespace RadAI.FoodFacilities.Data.Tests.Fixture
{
    public class FoodFacilitiesBdContextFixture : IDisposable
    {
        public readonly TestFoodFacilitiesDbContext FoodFacilitiesDbContext;

        public FoodFacilitiesBdContextFixture()
        {
            var options = new DbContextOptionsBuilder<FoodFacilitiesDbContext>()
                .UseInMemoryDatabase("testdatabase")
                .Options;

            FoodFacilitiesDbContext = new TestFoodFacilitiesDbContext(options);
        }

        public void Dispose()
        {
            FoodFacilitiesDbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class TestFoodFacilitiesDbContext : FoodFacilitiesDbContext
    {
        public TestFoodFacilitiesDbContext(DbContextOptions<FoodFacilitiesDbContext> options) : base(options)
        {
        }

        public DbSet<Entity> Entities { get; set; }
    }
}
