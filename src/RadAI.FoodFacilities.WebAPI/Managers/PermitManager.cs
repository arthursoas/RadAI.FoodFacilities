using RadAI.FoodFacilities.Data.Repositories;
using RadAI.FoodFacilities.DTOs.Entities;
using RadAI.FoodFacilities.WebAPI.Providers;
using RadAI.FoodFacilities.WebAPI.Settings;

namespace RadAI.FoodFacilities.WebAPI.Managers
{
    public class PermitManager : IPermitManager
    {
        private readonly IPermitRepository _permitRepository;
        private readonly IDataSFProvider _dataSFProvider;
        private readonly IDomainSettings _domainSettings;
        private DateTimeOffset? _lastCacheRefresh;

        public PermitManager(
            IPermitRepository permitRepository,
            IDataSFProvider dataSFProvider,
            IDomainSettings domainSettings)
        {
            _dataSFProvider = dataSFProvider;
            _permitRepository = permitRepository;
            _domainSettings = domainSettings;
        }

        public async Task<ICollection<Permit>> GetPermitsAsync(CancellationToken cancellationToken)
        {
            return await _dataSFProvider.GetMobileFoodFacilityPermitsAsync(
                cancellationToken);
        }
    }
}
