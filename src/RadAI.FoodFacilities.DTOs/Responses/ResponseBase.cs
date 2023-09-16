using FluentValidation.Results;

namespace RadAI.FoodFacilities.DTOs.Responses
{
    public class ResponseBase<TPayload>
    {
        public TPayload? Payload { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public ResponseBase()
        {
            ValidationResult = new ValidationResult();
        }
    }
}
