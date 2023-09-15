using RadAI.FoodFacilities.DTOs.Entities;
using System.Linq.Expressions;

namespace RadAI.FoodFacilities.WebAPI.Managers
{
    public interface IPermitManager
    {
        Task<ICollection<Permit>> GetPermitsAsync(Expression<Func<Permit, bool>> predicate, CancellationToken cancellationToken);
    }
}
