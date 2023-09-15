using Microsoft.EntityFrameworkCore;

namespace RadAI.FoodFacilities.Data.Repositories
{
    public abstract class RepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet;
        private readonly FoodFacilitiesDbContext _dbContext;

        public RepositoryBase(FoodFacilitiesDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.CommitAsync(cancellationToken);
        }
    }
}
