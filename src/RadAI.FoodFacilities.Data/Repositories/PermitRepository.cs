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
