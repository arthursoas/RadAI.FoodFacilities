using RadAI.FoodFacilities.DTOs.Requests.Permit;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.WebAPI.Managers;
using System.Linq.Expressions;
using PermitEntity = RadAI.FoodFacilities.DTOs.Entities.Permit;

namespace RadAI.FoodFacilities.WebAPI.Handlers.Permit
{
    public class GetPermitsByAddressHandler : HandlerBase<GetPermitsByAddressRequest, GetPermitResponse[]>
    { 
        private readonly IPermitManager _permitManager;

        public GetPermitsByAddressHandler(
            IPermitManager permitManager)
        {
            _permitManager = permitManager;
        }

        public override async Task<GetPermitResponse[]> HandleAsync(GetPermitsByAddressRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<PermitEntity, bool>> predicate = (p) => 
                p.Address != null &&
                p.Address.Contains(request.Address, StringComparison.OrdinalIgnoreCase);
           
            var permits = await _permitManager.GetPermitsAsync(predicate, cancellationToken);

            return permits.Select(p => GetPermitResponse.FromPermit(p)).ToArray();
        }
    }
}
