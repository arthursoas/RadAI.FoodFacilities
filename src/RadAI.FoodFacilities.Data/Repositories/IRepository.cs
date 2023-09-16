using RadAI.FoodFacilities.DTOs.Entities;
using System.Linq.Expressions;

namespace RadAI.FoodFacilities.Data.Repositories
{
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Saves the changes on the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        Task<bool> CommitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Indicates if there is any entity stored on the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>true if any or false if none</returns>
        public Task<bool> AnyAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Add a colletion of entities to the database
        /// </summary>
        /// <param name="entities">Collection of entities</param>
        /// <param name="cancellationToken">Cancelaation token</param>
        public Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Get entities that matches an expression
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of entities</returns>
        public Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Remove all entities from the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task RemoveAllAsync(CancellationToken cancellationToken);
    }
}
