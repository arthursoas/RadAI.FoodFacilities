using FluentValidation;
using RadAI.FoodFacilities.DTOs.Requests.Permit;

namespace RadAI.FoodFacilities.DTOs.Validations.Permit
{
    public class GetPermitsByCoordinateValidation : AbstractValidator<GetPermitsByCoordinateRequest>
    {
        public GetPermitsByCoordinateValidation()
        {
            RuleFor(p => p.Latitude)
                .NotNull()
                .GreaterThanOrEqualTo(-90)
                .LessThanOrEqualTo(90);

            RuleFor(p => p.Longitude)
                .NotNull()
                .GreaterThanOrEqualTo(-180)
                .LessThanOrEqualTo(180);
        }
    }
}
