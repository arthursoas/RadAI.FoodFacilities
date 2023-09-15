using RadAI.FoodFacilities.DTOs.Entities;

namespace RadAI.FoodFacilities.Data.Repositories
{
    public class PermitRepository : RepositoryBase<Permit>, IPermitRepository
    {
        public PermitRepository(FoodFacilitiesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
