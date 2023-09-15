using FluentValidation;
using RadAI.FoodFacilities.DTOs.Requests.Permit;

namespace RadAI.FoodFacilities.DTOs.Validations.Permit
{
    public class GetPermitByAddressValidation : AbstractValidator<GetPermitByAddressRequest>
    {
        public GetPermitByAddressValidation()
        {
            RuleFor(p => p.Address)
                .NotEmpty()
                .NotNull();
        }
    }
}
