using Microsoft.EntityFrameworkCore;
using RadAI.FoodFacilities.Data.Repositories;
using RadAI.FoodFacilities.Data.Tests.Fixture;
using Shouldly;
using System.Linq.Expressions;

namespace RadAI.FoodFacilities.Data.Tests
{
    public class TestRepository : RepositoryBase<Entity>, IRepository<Entity>
    {
        public TestRepository(FoodFacilitiesDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class RepositoryBaseTests : IClassFixture<FoodFacilitiesBdContextFixture>
    {
        private readonly FoodFacilitiesBdContextFixture _foodFacilitiesBdContextFixture;
        private readonly IRepository<Entity> _repository;

        const int NUMBER_OF_SEED_ENTRIES = 5;

        public RepositoryBaseTests(FoodFacilitiesBdContextFixture foodFacilitiesBdContextFixture)
        {
            _foodFacilitiesBdContextFixture = foodFacilitiesBdContextFixture;
            _repository = new TestRepository(_foodFacilitiesBdContextFixture.FoodFacilitiesDbContext);
            SeedDatabase();
        }

        [Fact]
        public async Task AnyAsync_WhenThereIsDataOnDatabase_ShouldReturnTrue()
        {
            // Act
            var any = await _repository.AnyAsync(CancellationToken.None);

            // Assert
            any.ShouldBeTrue();
        }

        [Fact]
        public async Task AnyAsync_WhenThereIsNoDataOnDatabase_ShouldReturnFalse()
        {
            // Arrange
            await _repository.RemoveAllAsync(CancellationToken.None);

            // Act
            var any = await _repository.AnyAsync(CancellationToken.None);

            // Assert
            any.ShouldBeTrue();
        }

        [Fact]
        public async Task AddRangeAsync_ShouldAddItemsToTheDatabase()
        {
            // Arrange
            var entity = new Entity
            {
                Id = "scenario",
                Name = "scenario name",
                Description = "scenario description"
            };
            var collection = new List<Entity> { entity };

            // Act
            await _repository.AddRangeAsync(collection, CancellationToken.None);
            await _repository.CommitAsync(CancellationToken.None);

            // Asssert
            var storedEntities = await _repository.ListAsync(null, CancellationToken.None);
            storedEntities.Any(e => e.Id.Equals("scenario")).ShouldBeTrue();
        }

        [Fact]
        public async Task ListAsync_WhenNoPredicateIsPresent_ShouldReturnAllEntities()
        {
            // Act
            var entities = await _repository.ListAsync(null, CancellationToken.None);

            // Arrange
            entities.Count.ShouldBe(NUMBER_OF_SEED_ENTRIES);
        }

        [Fact]
        public async Task ListAsync_WhenPredicateIsPresent_ShouldApplyFilter()
        {
            // Arrange
            Expression<Func<Entity, bool>> predicate = (p) => p.Name.Contains('0');

            // Act
            var entities = await _repository.ListAsync(predicate, CancellationToken.None);

            // Arrange
            entities.All(e => e.Name.Contains('0')).ShouldBeTrue();
        }

        [Fact]
        public async Task RemoveAllAsync_ShouldRemoveAllEntities()
        {
            // Act
            await _repository.RemoveAllAsync(CancellationToken.None);
            await _repository.CommitAsync(CancellationToken.None);

            // Assert
            var storedEntities = await _repository.ListAsync(null, CancellationToken.None);
            storedEntities.ShouldBeEmpty();
        }

        private void SeedDatabase()
        {
            _foodFacilitiesBdContextFixture.FoodFacilitiesDbContext.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(e => e.State = EntityState.Detached);

            _foodFacilitiesBdContextFixture.FoodFacilitiesDbContext.Database.EnsureDeleted();
            _foodFacilitiesBdContextFixture.FoodFacilitiesDbContext.Database.EnsureCreated();

            for (var index = 0; index < NUMBER_OF_SEED_ENTRIES; index++)
            {
                _foodFacilitiesBdContextFixture.FoodFacilitiesDbContext.Add(new Entity
                {
                    Id = $"{index}",
                    Name = $"Entity {index}",
                    Description = $"description of entity {index}"
                });
            }

            _foodFacilitiesBdContextFixture.FoodFacilitiesDbContext.SaveChanges();
        }
    }
}
