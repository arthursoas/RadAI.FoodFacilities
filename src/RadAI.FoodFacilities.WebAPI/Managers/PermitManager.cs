using RadAI.FoodFacilities.Data.Repositories;
using RadAI.FoodFacilities.DTOs.Entities;
using RadAI.FoodFacilities.WebAPI.Providers;
using RadAI.FoodFacilities.WebAPI.Settings;
using RadAI.FoodFacilities.WebAPI.Utils;
using System.Linq.Expressions;

namespace RadAI.FoodFacilities.WebAPI.Managers
{
    public class PermitManager : IPermitManager
    {
        private readonly IPermitRepository _permitRepository;
        private readonly IDataSFProvider _dataSFProvider;
        private readonly IDomainSettings _domainSettings;
        private readonly IDateTimeOffset _dateTimeOffset;

        public PermitManager(
            IPermitRepository permitRepository,
            IDataSFProvider dataSFProvider,
            IDomainSettings domainSettings,
            IDateTimeOffset dateTimeOffset)
        {
            _dataSFProvider = dataSFProvider;
            _permitRepository = permitRepository;
            _domainSettings = domainSettings;
            _dateTimeOffset = dateTimeOffset;
        }

        public async Task<ICollection<Permit>> GetPermitsAsync(Expression<Func<Permit, bool>>? predicate, CancellationToken cancellationToken)
        {
            await EnsureUpToDateCacheAsync(cancellationToken);

            return await _permitRepository.ListAsync(predicate, cancellationToken);
        }

        public async Task<ICollection<Permit>> GetPermitsByDistanceAsync(Expression<Func<Permit, bool>>? predicate, double latitude, double longitude, int take, CancellationToken cancellationToken)
        {
            await EnsureUpToDateCacheAsync(cancellationToken);

            return await _permitRepository.GetByDistanceAsync(predicate, latitude, longitude, take, cancellationToken);
        }

        private async Task EnsureUpToDateCacheAsync(CancellationToken cancellationToken)
        {
            var cacheExpired = _dateTimeOffset.UtcNow > _dataSFProvider
                .LastGetMobileFoodFacilityPermitsRequest
                .Add(_domainSettings.DataSFCacheExpiration);

            if (cacheExpired || !await _permitRepository.AnyAsync(cancellationToken))
            {
                var permits = await _dataSFProvider.GetMobileFoodFacilityPermitsAsync(cancellationToken);

                await _permitRepository.RemoveAllAsync(cancellationToken);
                await _permitRepository.AddRangeAsync(permits, cancellationToken);
                await _permitRepository.CommitAsync(cancellationToken);
            }
        }
    }
}
