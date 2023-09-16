using FluentValidation.Results;
using RadAI.FoodFacilities.DTOs.Responses;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.DTOs.Validations.Permit;

namespace RadAI.FoodFacilities.DTOs.Requests.Permit
{
    public class GetPermitsByAddressRequest : RequestBase<ResponseBase<GetPermitResponse[]>>
    {
        public string? Address { get; set; }

        public override ValidationResult Validate()
        {
            return new GetPermitsByAddressValidation().Validate(this);
        }
    }
}
