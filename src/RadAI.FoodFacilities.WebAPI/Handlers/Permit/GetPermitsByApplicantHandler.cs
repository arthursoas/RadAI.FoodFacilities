using RadAI.FoodFacilities.DTOs.Requests.Permit;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.WebAPI.Managers;
using System.Linq.Expressions;
using PermitEntity = RadAI.FoodFacilities.DTOs.Entities.Permit;

namespace RadAI.FoodFacilities.WebAPI.Handlers.Permit
{
    public class GetPermitsByApplicantHandler : HandlerBase<GetPermitsByApplicantRequest, GetPermitResponse[]>
    {
        private readonly IPermitManager _permitManager;

        public GetPermitsByApplicantHandler(
            IPermitManager permitManager)
        {
            _permitManager = permitManager;
        }

        public override async Task<GetPermitResponse[]> HandleAsync(GetPermitsByApplicantRequest request, CancellationToken cancellationToken)
        {
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

            return permits.Select(p => GetPermitResponse.FromPermit(p)).ToArray();
        }
    }
}
