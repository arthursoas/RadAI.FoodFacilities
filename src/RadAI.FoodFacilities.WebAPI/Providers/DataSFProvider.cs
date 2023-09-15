using RadAI.FoodFacilities.DTOs.Entities;
using RadAI.FoodFacilities.WebAPI.Settings;
using System.Text.Json;

namespace RadAI.FoodFacilities.WebAPI.Providers
{
    public class DataSFProvider : IDataSFProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IDomainSettings _domainSettings;

        public DataSFProvider(
            IHttpClientFactory httpClientFactory,
            IDomainSettings domainSettings)
        {
            _httpClient = httpClientFactory.CreateClient("datasf");
            _domainSettings = domainSettings;
        }

        public async Task<ICollection<Permit>> GetMobileFoodFacilityPermitsAsync(CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, _domainSettings.DataSFMobileFoodFacilitiesPermitsUrl);
            var responseMessage = await _httpClient.SendAsync(request, cancellationToken);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    "The HTTP request for Mobile Food Facility Permits failed",
                    inner: null,
                    statusCode: responseMessage.StatusCode);
            }

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await responseMessage.Content.ReadFromJsonAsync<Permit[]>(jsonOptions, cancellationToken) ?? Array.Empty<Permit>();
        }
    }
}
