using Microsoft.EntityFrameworkCore;
using RadAI.FoodFacilities.Domain.Entities;

namespace RadAI.FoodFacilities.Data
{
    public class FoodFacilitiesDbContext : DbContext
    {
        public FoodFacilitiesDbContext(DbContextOptions<FoodFacilitiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Permit> Permits { get; set; }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            return await SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
