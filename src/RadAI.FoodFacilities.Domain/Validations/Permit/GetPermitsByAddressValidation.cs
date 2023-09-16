using FluentValidation;
using RadAI.FoodFacilities.DTOs.Requests.Permit;

namespace RadAI.FoodFacilities.DTOs.Validations.Permit
{
    public class GetPermitsByAddressValidation : AbstractValidator<GetPermitsByAddressRequest>
    {
        public GetPermitsByAddressValidation()
        {
            RuleFor(p => p.Address)
                .NotEmpty()
                .NotNull();
        }
    }
}
