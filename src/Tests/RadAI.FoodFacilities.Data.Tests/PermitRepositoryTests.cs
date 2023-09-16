using Microsoft.EntityFrameworkCore;
using RadAI.FoodFacilities.Data.Repositories;
using RadAI.FoodFacilities.Data.Tests.Fixture;
using RadAI.FoodFacilities.DTOs.Entities;
using Shouldly;
using System.Linq.Expressions;

namespace RadAI.FoodFacilities.Data.Tests
{
    public class PermitRepositoryTests : IClassFixture<FoodFacilitiesBdContextFixture>
    {
        private readonly FoodFacilitiesBdContextFixture _foodFacilitiesBdContextFixture;
        private readonly IPermitRepository _permitRepository;

        const int NUMBER_OF_SEED_ENTRIES = 10;

        public PermitRepositoryTests(FoodFacilitiesBdContextFixture foodFacilitiesBdContextFixture)
        {
            _foodFacilitiesBdContextFixture = foodFacilitiesBdContextFixture;
            _permitRepository = new PermitRepository(_foodFacilitiesBdContextFixture.FoodFacilitiesDbContext);
            SeedDatabase();
        }

        [Fact]
        public async Task GetByDistanceAsync_WhenThereIsNoPredicate_ShouldReturnClosestPermits()
        {
            // Arrange
            const double LATITUDE = 2.0;
            const double LONGITUDE = 2.0;
            const int TAKE = 5;

            // Act
            var permits = await _permitRepository.GetByDistanceAsync(
                null,
                LATITUDE,
                LONGITUDE,
                TAKE,
                CancellationToken.None);

            // Assert
            permits.Count.ShouldBe(TAKE);
            permits.All(p => new double[] { 0, 1, 2, 3, 4 }.Contains(p.Latitude)).ShouldBeTrue();
        }

        [Fact]
        public async Task GetByDistanceAsync_WhenThereIsPredicate_ShouldApplyFilters()
        {
            // Arrange
            const double LATITUDE = 2.0;
            const double LONGITUDE = 2.0;
            const int TAKE = 5;

            Expression<Func<Permit, bool>> predicate = (p) => p.Latitude >= 7;

            // Act
            var permits = await _permitRepository.GetByDistanceAsync(
                predicate,
                LATITUDE,
                LONGITUDE,
                TAKE,
                CancellationToken.None);

            // Assert
            permits.All(p => new double[] { 7, 8, 9 }.Contains(p.Latitude)).ShouldBeTrue();
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
                _foodFacilitiesBdContextFixture.FoodFacilitiesDbContext.Add(new Permit
                {
                    Id = $"{index}",
                    Applicant = $"Applicant {index}",
                    Latitude = index,
                    Longitude = index,
                });
            }

            _foodFacilitiesBdContextFixture.FoodFacilitiesDbContext.SaveChanges();
        }
    }
}
