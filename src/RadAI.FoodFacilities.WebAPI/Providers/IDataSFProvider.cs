using RadAI.FoodFacilities.DTOs.Entities;

namespace RadAI.FoodFacilities.WebAPI.Providers
{
    public interface IDataSFProvider
    {
        /// <summary>
        /// Gets the list of permits of mobile foods facilities from the city of San Francisco
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Collection of permits</returns>
        public Task<ICollection<Permit>> GetMobileFoodFacilityPermitsAsync(CancellationToken cancellationToken);
    }
}
