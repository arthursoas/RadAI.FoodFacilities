using FluentValidation.Results;
using MediatR;

namespace RadAI.FoodFacilities.DTOs.Requests
{
    public abstract class RequestBase<TResponse> : IRequest<TResponse>
    {
        public abstract ValidationResult Validate();
    }
}
