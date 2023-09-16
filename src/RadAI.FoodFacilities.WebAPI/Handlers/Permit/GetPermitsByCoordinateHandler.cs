using MediatR;
using RadAI.FoodFacilities.DTOs.Requests.Permit;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.DTOs.Responses;
using RadAI.FoodFacilities.WebAPI.Managers;
using System.Linq.Expressions;
using PermitEntity = RadAI.FoodFacilities.DTOs.Entities.Permit;

namespace RadAI.FoodFacilities.WebAPI.Handlers.Permit
{
    public class GetPermitsByCoordinateHandler : HandlerBase, IRequestHandler<GetPermitsByCoordinateRequest, ResponseBase<GetPermitResponse[]>>
    {
        private readonly IPermitManager _permitManager;

        public GetPermitsByCoordinateHandler(
            IPermitManager permitManager)
        {
            _permitManager = permitManager;
        }

        public async Task<ResponseBase<GetPermitResponse[]>> Handle(GetPermitsByCoordinateRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<GetPermitResponse[]>();
            var validation = request.Validate();

            if (!validation.IsValid)
            {
                response.ValidationResult = validation;
                return response;
            }

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
