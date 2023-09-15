using FluentValidation;
using RadAI.FoodFacilities.DTOs.Requests.Permit;

namespace RadAI.FoodFacilities.DTOs.Validations.Permit
{
    public class GetPermitByApplicantValidation : AbstractValidator<GetPermitByApplicantRequest>
    {
        public GetPermitByApplicantValidation()
        {
            RuleFor(p => p.Applicant)
                .NotEmpty()
                .NotNull();
        }
    }
}
