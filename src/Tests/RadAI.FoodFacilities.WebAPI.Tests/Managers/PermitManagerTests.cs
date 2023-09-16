using NSubstitute;
using RadAI.FoodFacilities.Data.Repositories;
using RadAI.FoodFacilities.DTOs.Entities;
using RadAI.FoodFacilities.WebAPI.Managers;
using RadAI.FoodFacilities.WebAPI.Providers;
using RadAI.FoodFacilities.WebAPI.Settings;
using RadAI.FoodFacilities.WebAPI.Utils;
using Shouldly;
using System.Linq.Expressions;

namespace RadAI.FoodFacilities.WebAPI.Tests.Managers
{
    public class PermitManagerTests
    {
        private readonly IPermitRepository _permitRepository;
        private readonly IDataSFProvider _dataSFProvider;
        private readonly IDomainSettings _domainSettings;
        private readonly IDateTimeOffset _dateTimeOffset;
        private readonly IPermitManager _permitManager;

        public PermitManagerTests()
        {
            _permitRepository = Substitute.For<IPermitRepository>();
            _dataSFProvider = Substitute.For<IDataSFProvider>();
            _domainSettings = Substitute.For<IDomainSettings>();
            _dateTimeOffset = Substitute.For<IDateTimeOffset>();

            _domainSettings
                .DataSFCacheExpiration
                .Returns(TimeSpan.FromSeconds(60));

            _dateTimeOffset
                .UtcNow
                .Returns(new DateTimeOffset(2020, 10, 10, 12, 0, 0, TimeSpan.Zero));

            _permitManager = new PermitManager(
                _permitRepository,
                _dataSFProvider,
                _domainSettings,
                _dateTimeOffset);

            MockCacheUpdate();
        }

        [Fact]
        public async Task GetPermitsAsync_WhenThereIsNoCache_ShouldGetDataFromProvider()
        {
            // Arrange
            _permitRepository
                .AnyAsync(CancellationToken.None)
                .Returns(false);

            // Act
            var pemrits = await _permitManager.GetPermitsAsync(
                null,
                CancellationToken.None);

            // Assert
            pemrits.Count.ShouldBe(3);

            await _dataSFProvider
                .Received(1)
                .GetMobileFoodFacilityPermitsAsync(CancellationToken.None);

            await _permitRepository
                .Received(1)
                .CommitAsync(CancellationToken.None);
        }

        [Fact]
        public async Task GetPermitsAsync_WhenCacheIsExpired_ShouldGetDataFromProvider()
        {
            // Arrange
            _permitRepository
                .AnyAsync(CancellationToken.None)
                .Returns(true);
            _dataSFProvider
                .LastGetMobileFoodFacilityPermitsRequest
                .Returns(_dateTimeOffset.UtcNow.AddSeconds(-120));

            // Act
            var pemrits = await _permitManager.GetPermitsAsync(
                null,
                CancellationToken.None);

            // Assert
            pemrits.Count.ShouldBe(3);

            await _dataSFProvider
                .Received(1)
                .GetMobileFoodFacilityPermitsAsync(CancellationToken.None);

            await _permitRepository
                .Received(1)
                .CommitAsync(CancellationToken.None);
        }

        [Fact]
        public async Task GetPermitsAsync_WhenCacheIsNotExpired_ShouldGetDataFromDatabase()
        {
            // Arrange
            _permitRepository
                .AnyAsync(CancellationToken.None)
                .Returns(true);
            _dataSFProvider
                .LastGetMobileFoodFacilityPermitsRequest
                .Returns(new DateTimeOffset(2020, 10, 10, 12, 0, 0, TimeSpan.Zero));

            Expression<Func<Permit, bool>> predicate = p => true;

            // Act
            var pemrits = await _permitManager.GetPermitsAsync(
                predicate,
                CancellationToken.None);

            // Assert
            pemrits.Count.ShouldBe(3);

            await _dataSFProvider
                .DidNotReceive()
                .GetMobileFoodFacilityPermitsAsync(CancellationToken.None);

            await _permitRepository
                .Received(1)
                .ListAsync(
                    predicate,
                    CancellationToken.None);
        }

        [Fact]
        public async Task GetPermitsByDistanceAsync_WhenThereIsNoCache_ShouldGetDataFromProvider()
        {
            // Arrange
            _permitRepository
                .AnyAsync(CancellationToken.None)
                .Returns(false);

            // Act
            var pemrits = await _permitManager.GetPermitsByDistanceAsync(
                null,
                0, 0,
                5,
                CancellationToken.None);

            // Assert
            pemrits.Count.ShouldBe(3);

            await _dataSFProvider
                .Received(1)
                .GetMobileFoodFacilityPermitsAsync(CancellationToken.None);

            await _permitRepository
                .Received(1)
                .CommitAsync(CancellationToken.None);
        }

        [Fact]
        public async Task GetPermitsByDistanceAsync_WhenCacheIsExpired_ShouldGetDataFromProvider()
        {
            // Arrange
            _permitRepository
                .AnyAsync(CancellationToken.None)
                .Returns(true);
            _dataSFProvider
                .LastGetMobileFoodFacilityPermitsRequest
                .Returns(_dateTimeOffset.UtcNow.AddSeconds(-120));

            // Act
            var pemrits = await _permitManager.GetPermitsByDistanceAsync(
                null,
                0, 0,
                5,
                CancellationToken.None);

            // Assert
            pemrits.Count.ShouldBe(3);

            await _dataSFProvider
                .Received(1)
                .GetMobileFoodFacilityPermitsAsync(CancellationToken.None);

            await _permitRepository
                .Received(1)
                .CommitAsync(CancellationToken.None);
        }

        [Fact]
        public async Task GetPermitsByDistanceAsync_WhenCacheIsNotExpired_ShouldGetDataFromDatabase()
        {
            // Arrange
            _permitRepository
                .AnyAsync(CancellationToken.None)
                .Returns(true);
            _dataSFProvider
                .LastGetMobileFoodFacilityPermitsRequest
                .Returns(new DateTimeOffset(2020, 10, 10, 12, 0, 0, TimeSpan.Zero));

            Expression<Func<Permit, bool>> predicate = p => true;

            // Act
            var pemrits = await _permitManager.GetPermitsByDistanceAsync(
                predicate,
                0, 0,
                5,
                CancellationToken.None);

            // Assert
            pemrits.Count.ShouldBe(3);

            await _dataSFProvider
                .DidNotReceive()
                .GetMobileFoodFacilityPermitsAsync(CancellationToken.None);

            await _permitRepository
                .Received(1)
                .GetByDistanceAsync(
                    predicate,
                    0, 0,
                    5,
                    CancellationToken.None);
        }

        private void MockCacheUpdate()
        {
            var permits = new List<Permit>
            {
                new Permit
                {
                    Id = "1",
                    Applicant = "STUART LITTLE",
                    Status = "APPROVED",
                    Address = "ST FLORIANO PEIXOTO"
                },
                new Permit
                {
                    Id = "2",
                    Applicant = "STUART LITTLE",
                    Status = "APPROVED",
                    Address = "ST MIGUEL BURNIER"
                },
                new Permit
                {
                    Id = "3",
                    Applicant = "STUART LITTLE",
                    Status = "APPROVED",
                    Address = "ST PRIMAVERA"
                },
            };

            _dataSFProvider
                .GetMobileFoodFacilityPermitsAsync(CancellationToken.None)
                .Returns(permits);

            _permitRepository
                .ListAsync(
                    Arg.Any<Expression<Func<Permit, bool>>>(),
                    CancellationToken.None)
                .Returns(permits);

            _permitRepository
                .GetByDistanceAsync(
                    Arg.Any<Expression<Func<Permit, bool>>>(),
                    Arg.Any<double>(),
                    Arg.Any<double>(),
                    Arg.Any<int>(),
                    CancellationToken.None)
                .Returns(permits);
        }
    }
}
