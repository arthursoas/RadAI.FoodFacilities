using RadAI.FoodFacilities.DTOs.Entities;
using System.Linq.Expressions;

namespace RadAI.FoodFacilities.Data.Repositories
{
    public interface IPermitRepository : IRepository<Permit>
    {
        /// <summary>
        /// List the n permits closest to a coordinate
        /// </summary>
        /// <param name="predicate">Expression to filter permits</param>
        /// <param name="latitude">Coordinate latitude</param>
        /// <param name="longitude">Coordinate longitude</param>
        /// <param name="take">Number of permits</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of permits</returns>
        public Task<ICollection<Permit>> GetByDistanceAsync(Expression<Func<Permit, bool>> predicate, double latitude, double longitude, int take, CancellationToken cancellationToken);
    }
}
