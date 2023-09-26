using FluentValidation.Results;
using MediatR;
using RadAI.FoodFacilities.DTOs.Responses;

namespace RadAI.FoodFacilities.DTOs.Requests
{
    public abstract class RequestBase<TResponse> : IRequest<ResponseBase<TResponse>>
    {
        public abstract ValidationResult Validate();
    }
}
