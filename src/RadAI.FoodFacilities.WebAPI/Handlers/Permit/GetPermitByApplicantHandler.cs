using MediatR;
using RadAI.FoodFacilities.DTOs.Requests.Permit;
using RadAI.FoodFacilities.DTOs.Responses;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.WebAPI.Managers;
using System.Linq.Expressions;
using PermitEntity = RadAI.FoodFacilities.DTOs.Entities.Permit;

namespace RadAI.FoodFacilities.WebAPI.Handlers.Permit
{
    public class GetPermitByApplicantHandler : HandlerBase, IRequestHandler<GetPermitByApplicantRequest, ResponseBase<GetPermitResponse[]>>
    {
        private readonly IPermitManager _permitManager;

        public GetPermitByApplicantHandler(
            IPermitManager permitManager)
        {
            _permitManager = permitManager;
        }

        public async Task<ResponseBase<GetPermitResponse[]>> Handle(GetPermitByApplicantRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<GetPermitResponse[]>();
            var validation = request.Validate();

            if (!validation.IsValid)
            {
                response.ValidationResult = validation;
                return response;
            }

            Expression<Func<PermitEntity, bool>> predicate;
            if (request.Status == null)
            {
                predicate = (p) =>
                    request.Applicant.Equals(p.Applicant, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                predicate = (p) => 
                    request.Applicant.Equals(p.Applicant, StringComparison.OrdinalIgnoreCase) &&
                    request.Status.Equals(p.Status, StringComparison.OrdinalIgnoreCase);
            }

            var permits = await _permitManager.GetPermitsAsync(predicate, cancellationToken);

            return new ResponseBase<GetPermitResponse[]>
            {
                Payload = permits.Select(p => GetPermitResponse.FromPermit(p)).ToArray()
            };
        }
    }
}
