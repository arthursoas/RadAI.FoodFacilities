using RadAI.FoodFacilities.DTOs.Requests.Permit;
using RadAI.FoodFacilities.DTOs.Responses;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.WebAPI.Managers;
using System.Linq.Expressions;
using PermitEntity = RadAI.FoodFacilities.DTOs.Entities.Permit;

namespace RadAI.FoodFacilities.WebAPI.Handlers.Permit
{
    public class GetPermitsByCoordinateHandler : HandlerBase<GetPermitsByCoordinateRequest, GetPermitResponse[]>
    {
        private readonly IPermitManager _permitManager;

        public GetPermitsByCoordinateHandler(
            IPermitManager permitManager)
        {
            _permitManager = permitManager;
        }

        public override async Task<ResponseBase<GetPermitResponse[]>> HandleAsync(GetPermitsByCoordinateRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<GetPermitResponse[]>();

            const int DEAFULT_LIMIT = 5;

            Expression<Func<PermitEntity, bool>> predicate = (p) =>
                request.Status.Equals(p.Status, StringComparison.OrdinalIgnoreCase) &&
                request.FacilityType.Equals(p.FacilityType, StringComparison.OrdinalIgnoreCase);

            
            var permits = await _permitManager.GetPermitsByDistanceAsync(
                predicate,
                request.Latitude ?? 0,
                request.Longitude ?? 0,
                DEAFULT_LIMIT,
                cancellationToken);

            return new ResponseBase<GetPermitResponse[]>
            {
                Payload = permits.Select(p => GetPermitResponse.FromPermit(p)).ToArray()
            };
        }
    }
}
