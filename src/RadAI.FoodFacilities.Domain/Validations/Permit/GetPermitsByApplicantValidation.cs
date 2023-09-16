using FluentValidation;
using RadAI.FoodFacilities.DTOs.Requests.Permit;

namespace RadAI.FoodFacilities.DTOs.Validations.Permit
{
    public class GetPermitsByApplicantValidation : AbstractValidator<GetPermitsByApplicantRequest>
    {
        public GetPermitsByApplicantValidation()
        {
            RuleFor(p => p.Applicant)
                .NotEmpty()
                .NotNull();
        }
    }
}
