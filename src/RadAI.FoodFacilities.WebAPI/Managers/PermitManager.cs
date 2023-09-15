using RadAI.FoodFacilities.Data.Repositories;
using RadAI.FoodFacilities.DTOs.Entities;
using RadAI.FoodFacilities.WebAPI.Providers;
using RadAI.FoodFacilities.WebAPI.Settings;
using System.Linq.Expressions;

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

        public async Task<ICollection<Permit>> GetPermitsAsync(Expression<Func<Permit, bool>> predicate, CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow;

            if (!await _permitRepository.AnyAsync(cancellationToken))
            {
                var permits = await _dataSFProvider.GetMobileFoodFacilityPermitsAsync(cancellationToken);

                await _permitRepository.RemoveAllAsync(cancellationToken);
                await _permitRepository.AddRangeAsync(permits, cancellationToken);
                await _permitRepository.CommitAsync(cancellationToken);

                _lastCacheRefresh = now;
            }

            return await _permitRepository.ListAsync(predicate, cancellationToken);
        }
    }
}
