using FluentValidation.Results;
using MediatR;
using RadAI.FoodFacilities.DTOs.Requests;
using RadAI.FoodFacilities.DTOs.Responses;

namespace RadAI.FoodFacilities.WebAPI.Handlers
{
    public abstract class HandlerBase<TRequest, TResponsePayload> : IRequestHandler<TRequest, ResponseBase<TResponsePayload>>
        where TRequest : RequestBase<ResponseBase<TResponsePayload>>
        where TResponsePayload : class
    {
        public async Task<ResponseBase<TResponsePayload>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<TResponsePayload>();
            var validation = request.Validate();

            if (!validation.IsValid)
            {
                response.ValidationResult = validation;
                return response;
            }

            return await HandleAsync(request, cancellationToken);
        }

        public abstract Task<ResponseBase<TResponsePayload>> HandleAsync(TRequest request, CancellationToken cancellationToken);

        public static ValidationFailure BuildError(string message)
        {
            return new ValidationFailure(string.Empty, message);
        }
    }
}
