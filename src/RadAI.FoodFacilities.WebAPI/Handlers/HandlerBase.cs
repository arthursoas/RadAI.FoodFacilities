using FluentValidation.Results;

namespace RadAI.FoodFacilities.WebAPI.Handlers
{
    public abstract class HandlerBase
    {
        public static ValidationFailure BuildError(string message)
        {
            return new ValidationFailure(string.Empty, message);
        }
    }
}
