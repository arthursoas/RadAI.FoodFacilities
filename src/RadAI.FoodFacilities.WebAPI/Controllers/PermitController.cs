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
        public async Task<ActionResult> ListPermitsByApplicantAsync([FromRoute] string applicant, [FromQuery] string status, CancellationToken cancellationToken)
        {
            var request = new GetPermitByApplicantRequest
            {
                Applicant = applicant?.Trim(),
                Status = status?.Trim(),
            };

            return BuildResponse(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("addresses/{address}")]
        public async Task<ActionResult> ListPermitsByAddressAsync([FromRoute] string address, CancellationToken cancellationToken)
        {
            var request = new GetPermitByAddressRequest
            {
                Address = address?.Trim()
            };

            return BuildResponse(await Mediator.Send(request, cancellationToken));
        }
    }
}
