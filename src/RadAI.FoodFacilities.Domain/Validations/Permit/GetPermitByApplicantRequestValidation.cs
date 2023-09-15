using FluentValidation;
using RadAI.FoodFacilities.DTOs.Requests.Permit;

namespace RadAI.FoodFacilities.DTOs.Validations.Permit
{
    public class GetPermitByApplicantRequestValidation : AbstractValidator<GetPermitByApplicantRequest>
    {
        public GetPermitByApplicantRequestValidation()
        {
            RuleFor(p => p.Applicant)
                .NotEmpty()
                .NotNull();
        }
    }
}
