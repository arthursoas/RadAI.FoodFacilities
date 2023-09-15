using MediatR;
using Microsoft.AspNetCore.Mvc;
using RadAI.FoodFacilities.DTOs.Requests.Permit;

namespace RadAI.FoodFacilities.WebAPI.Controllers
{
    [Route("api/permits")]
    public class PermitController : ControllerBase
    {
        public PermitController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor)
            : base(mediator, httpContextAccessor)
        {
        }

        [HttpGet("applicants/{applicant}")]
        public async Task<ActionResult> ListPermitsByApplicant([FromRoute] string applicant, [FromQuery] string status, CancellationToken cancellationToken)
        {
            var request = new GetPermitByApplicantRequest
            {
                Applicant = applicant,
                Status = status,
            };

            return BuildResponse(await Mediator.Send(request, cancellationToken));
        }
    }
}
