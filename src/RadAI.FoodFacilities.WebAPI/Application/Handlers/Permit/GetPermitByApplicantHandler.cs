using MediatR;
using RadAI.FoodFacilities.DTOs.Requests.Permit;
using RadAI.FoodFacilities.DTOs.Responses;
using RadAI.FoodFacilities.DTOs.Responses.Permit;

namespace RadAI.FoodFacilities.WebAPI.Application.Handlers.Permit
{
    public class GetPermitByApplicantHandler : HandlerBase, IRequestHandler<GetPermitByApplicantRequest, ResponseBase<GetPermitResponse[]>>
    {
        public async Task<ResponseBase<GetPermitResponse[]>> Handle(GetPermitByApplicantRequest request, CancellationToken cancellationToken)
        {
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
