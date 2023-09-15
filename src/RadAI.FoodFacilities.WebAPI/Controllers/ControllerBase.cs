using MediatR;
using Microsoft.AspNetCore.Mvc;
using RadAI.FoodFacilities.DTOs.Responses;

namespace RadAI.FoodFacilities.WebAPI.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly IMediator Mediator;
        protected readonly IHttpContextAccessor HttpContextAccessor;

        public ControllerBase(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor)
        {
            Mediator = mediator;
            HttpContextAccessor = httpContextAccessor;
        }

        protected ActionResult BuildResponse<TPayload>(ResponseBase<TPayload> response)
        {
            if (response.ValidationResult.Errors.Any())
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "messages", response.ValidationResult.Errors.Select(e => e.ErrorMessage).ToArray() }
                }));
            }

            return Ok(response.Payload);
        }
    }
}
