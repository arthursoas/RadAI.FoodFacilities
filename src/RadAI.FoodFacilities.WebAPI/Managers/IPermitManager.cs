using RadAI.FoodFacilities.DTOs.Entities;
using System.Linq.Expressions;

namespace RadAI.FoodFacilities.WebAPI.Managers
{
    public interface IPermitManager
    {
        /// <summary>
        /// Get a collection of permits based on a filter
        /// </summary>
        /// <param name="predicate">Expression to filter permits</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of permits</returns>
        Task<ICollection<Permit>> GetPermitsAsync(Expression<Func<Permit, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Get a collection of permits close to a coordinate
        /// </summary>
        /// <param name="predicate">Expression to filter permits</param>
        /// <param name="latitude">Coordinate latitude</param>
        /// <param name="longitude">Coordinate longitude</param>
        /// <param name="take">Number of permits to get</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of permits</returns>
        Task<ICollection<Permit>> GetPermitsByDistanceAsync(Expression<Func<Permit, bool>> predicate, double latitude, double longitude, int take, CancellationToken cancellationToken);
    }
}
