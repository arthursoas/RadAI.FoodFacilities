using NSubstitute;
using RadAI.FoodFacilities.WebAPI.Providers;
using RadAI.FoodFacilities.WebAPI.Settings;
using RadAI.FoodFacilities.WebAPI.Utils;
using Shouldly;
using System.Net;
using System.Reflection;

namespace RadAI.FoodFacilities.WebAPI.Tests.Providers
{
    public class DataSFProviderTests
    {
        private readonly DelegatingHandler _delegatingHandler;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDomainSettings _domainSettings;
        private readonly IDateTimeOffset _dateTimeOffset;
        private readonly IDataSFProvider _dataSFProvider;

        public DataSFProviderTests()
        {
            _httpClientFactory = Substitute.For<IHttpClientFactory>();
            _domainSettings = Substitute.For<IDomainSettings>();
            _dateTimeOffset = Substitute.For<IDateTimeOffset>();

            _delegatingHandler = Substitute.For<DelegatingHandler>();
            _delegatingHandler
                .Protected(
                    "SendAsync",
                    Arg.Any<HttpRequestMessage>(),
                    Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"objectid\":\"1\",\"applicant\":\"applicant\"}]")
                }));

            var client = new HttpClient(_delegatingHandler);
            _httpClientFactory
                .CreateClient(Arg.Any<string>())
                .Returns(client);

            _dateTimeOffset
                .UtcNow
                .Returns(new DateTimeOffset(2020, 10, 10, 12, 0, 0, TimeSpan.Zero));

            _domainSettings
                .DataSFMobileFoodFacilitiesPermitsUrl
                .Returns("https://fake.com");

            _dataSFProvider = new DataSFProvider(
                _httpClientFactory,
                _domainSettings,
                _dateTimeOffset);
        }

        [Fact]
        public async Task GetMobileFoodFacilityPermitsAsync_WhenRequestReturnData_ShoudReturnPermits()
        {
            // Act
            var permits = await _dataSFProvider.GetMobileFoodFacilityPermitsAsync(CancellationToken.None);

            // Assert
            permits.Count.ShouldBe(1);
            _dataSFProvider.LastGetMobileFoodFacilityPermitsRequest.ShouldBe(
                new DateTimeOffset(2020, 10, 10, 12, 0, 0, TimeSpan.Zero));
        }

        [Fact]
        public async Task GetMobileFoodFacilityPermitsAsync_WhenRequestFails_ShoudReturnPermits()
        {
            // Arrange
            _delegatingHandler
                .Protected(
                    "SendAsync",
                    Arg.Any<HttpRequestMessage>(),
                    Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("{\"error\":\"error\"}")
                }));

            // Act
            var permitsTask = _dataSFProvider.GetMobileFoodFacilityPermitsAsync(CancellationToken.None);

            // Assert
            var exception = await Should.ThrowAsync<HttpRequestException>(permitsTask);
            exception.Message.ShouldBe("The HTTP request for Mobile Food Facility Permits failed");
        }
    }

    public static class TestExtensions
    {
        public static object Protected(this object target, string name, params object[] args)
        {
            var type = target.GetType();
            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(x => x.Name == name).Single();
            return method.Invoke(target, args);
        }
    }
}

