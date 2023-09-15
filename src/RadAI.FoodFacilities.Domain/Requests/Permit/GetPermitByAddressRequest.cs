using FluentValidation.Results;
using RadAI.FoodFacilities.DTOs.Responses;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.DTOs.Validations.Permit;

namespace RadAI.FoodFacilities.DTOs.Requests.Permit
{
    public class GetPermitByAddressRequest : RequestBase<ResponseBase<GetPermitResponse[]>>
    {
        public string? Address { get; set; }

        public override ValidationResult Validate()
        {
            return new GetPermitByAddressValidation().Validate(this);
        }
    }
}
