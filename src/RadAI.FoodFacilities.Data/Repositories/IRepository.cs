namespace RadAI.FoodFacilities.Data.Repositories
{
    public interface IRepository
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken);
    }
}
