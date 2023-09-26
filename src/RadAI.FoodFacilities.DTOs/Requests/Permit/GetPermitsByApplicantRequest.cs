using FluentValidation.Results;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.DTOs.Validations.Permit;

namespace RadAI.FoodFacilities.DTOs.Requests.Permit
{
    public class GetPermitsByApplicantRequest : RequestBase<GetPermitResponse[]>
    {
        public string? Applicant { get; set; }

        public string? Status { get; set; }

        public override ValidationResult Validate()
        {
            return new GetPermitsByApplicantValidation().Validate(this);
        }
    }
}
