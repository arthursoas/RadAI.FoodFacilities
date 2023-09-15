using RadAI.FoodFacilities.DTOs.Entities;

namespace RadAI.FoodFacilities.WebAPI.Managers
{
    public interface IPermitManager
    {
        Task<ICollection<Permit>> GetPermitsAsync(CancellationToken cancellationToken);
    }
}
