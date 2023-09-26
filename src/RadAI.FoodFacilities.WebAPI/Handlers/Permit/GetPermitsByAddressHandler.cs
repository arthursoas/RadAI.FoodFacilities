using RadAI.FoodFacilities.DTOs.Requests.Permit;
using RadAI.FoodFacilities.DTOs.Responses;
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

        public override async Task<ResponseBase<GetPermitResponse[]>> HandleAsync(GetPermitsByAddressRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<GetPermitResponse[]>();

            Expression<Func<PermitEntity, bool>> predicate = (p) => 
                p.Address != null &&
                p.Address.Contains(request.Address, StringComparison.OrdinalIgnoreCase);
           
            var permits = await _permitManager.GetPermitsAsync(predicate, cancellationToken);

            return new ResponseBase<GetPermitResponse[]>
            {
                Payload = permits.Select(p => GetPermitResponse.FromPermit(p)).ToArray()
            };
        }
    }
}
