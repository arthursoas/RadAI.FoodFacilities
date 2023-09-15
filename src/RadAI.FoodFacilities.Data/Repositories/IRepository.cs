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
        /// Add a colletion of permits to the database
        /// </summary>
        /// <param name="permits">Collection of permits</param>
        /// <param name="cancellationToken">Cancelaation token</param>
        public Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Get permits that matches an expression
        /// </summary>
        /// <param name="predicate">Expression to filter permits</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of permits</returns>
        public Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Remove all permits from the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task RemoveAllAsync(CancellationToken cancellationToken);
    }
}
