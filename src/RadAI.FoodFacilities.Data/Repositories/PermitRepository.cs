using Microsoft.EntityFrameworkCore;
using RadAI.FoodFacilities.DTOs.Entities;
using System.Linq.Expressions;

namespace RadAI.FoodFacilities.Data.Repositories
{
    public class PermitRepository : RepositoryBase<Permit>, IPermitRepository
    {
        public PermitRepository(FoodFacilitiesDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// List the n permits closest to a coordinate  by using the distance between two points on a Cartesian plane
        /// Should be improved for a more precise formula if consider bigger areas
        /// </summary>
        /// <param name="predicate">Expression to filter permits</param>
        /// <param name="latitude">Coordinate latitude</param>
        /// <param name="longitude">Coordinate longitude</param>
        /// <param name="take">Number of permits</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of permits</returns>
        public async Task<ICollection<Permit>> GetByDistanceAsync(Expression<Func<Permit, bool>>? predicate, double latitude, double longitude, int take, CancellationToken cancellationToken)
        {
            predicate ??= e => true;

            return await DbSet
                .Where(predicate)
                .OrderBy(p => (latitude - p.Latitude) * (latitude - p.Latitude) + (longitude - p.Longitude) * (longitude - p.Longitude))
                .Take(take)
                .ToListAsync(cancellationToken);
        }
    }
}
