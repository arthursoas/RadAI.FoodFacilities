using FluentValidation.Results;
using RadAI.FoodFacilities.DTOs.Responses;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.DTOs.Validations.Permit;

namespace RadAI.FoodFacilities.DTOs.Requests.Permit
{
    public class GetPermitsByCoordinateRequest : RequestBase<ResponseBase<GetPermitResponse[]>>
    {
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string Status { get; set; }

        public string FacilityType { get; set; }

        public override ValidationResult Validate()
        {
            return new GetPermitsByCoordinateValidation().Validate(this);
        }
    }
}
