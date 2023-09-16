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
        public async Task<ActionResult> GetPermitsByApplicantAsync([FromRoute] string applicant, [FromQuery] string status, CancellationToken cancellationToken)
        {
            var request = new GetPermitsByApplicantRequest
            {
                Applicant = applicant?.Trim(),
                Status = status?.Trim(),
            };

            return BuildResponse(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("addresses/{address}")]
        public async Task<ActionResult> GetPermitsByAddressAsync([FromRoute] string address, CancellationToken cancellationToken)
        {
            var request = new GetPermitsByAddressRequest
            {
                Address = address?.Trim()
            };

            return BuildResponse(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("latitute/{latitude}/longitude/{longitude}")]
        public async Task<ActionResult> GetPermitsByCoordinatesAsync([FromRoute] double? latitude, [FromRoute] double? longitude, [FromQuery] string status, CancellationToken cancellationToken)
        {
            const string DEFAULT_STATUS = "APPROVED";
            const string DEFAULT_FACILITY_TYPE = "TRUCK";

            var request = new GetPermitsByCoordinateRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                Status = status ?? DEFAULT_STATUS,
                FacilityType = DEFAULT_FACILITY_TYPE
            };

            return BuildResponse(await Mediator.Send(request, cancellationToken));
        }
    }
}
