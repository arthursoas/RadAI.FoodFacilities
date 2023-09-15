using MediatR;
using RadAI.FoodFacilities.DTOs.Requests.Permit;
using RadAI.FoodFacilities.DTOs.Responses;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.WebAPI.Managers;

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

            var a = await _permitManager.GetPermitsAsync(cancellationToken);

            return new ResponseBase<GetPermitResponse[]>
            {
                Payload = new GetPermitResponse[]
                {
                    new GetPermitResponse
                    {
                        Id = "123",
                        Applicant = "Teste"
                    }
                }
            };
        }
    }
}
