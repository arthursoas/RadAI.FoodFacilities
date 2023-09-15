using Microsoft.EntityFrameworkCore;
using RadAI.FoodFacilities.DTOs.Entities;
using System.Linq.Expressions;

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

        public async Task<bool> AnyAsync(CancellationToken cancellationToken)
        {
            return await DbSet.AnyAsync(cancellationToken);
        }

        public async Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken)
        {
            await DbSet.AddRangeAsync(entities, cancellationToken);
        }

        public async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            if (predicate == null)
            {
                predicate = e => true;
            }

            return await DbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task RemoveAllAsync(CancellationToken cancellationToken)
        {
            var permits = await DbSet.ToListAsync(cancellationToken);
            DbSet.RemoveRange(permits);
        }
    }
}
