using FluentValidation.Results;
using MediatR;
using RadAI.FoodFacilities.DTOs.Requests;
using RadAI.FoodFacilities.DTOs.Responses;

namespace RadAI.FoodFacilities.WebAPI.Handlers
{
    public abstract class HandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, ResponseBase<TResponse>>
        where TRequest : RequestBase<TResponse>
        where TResponse : class
    {
        protected ResponseBase<TResponse>? Response;

        public async Task<ResponseBase<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Response = new ResponseBase<TResponse>();
            var validation = request.Validate();

            if (!validation.IsValid)
            {
                Response.ValidationResult = validation;
                return Response;
            }

            Response.Payload = await HandleAsync(request, cancellationToken);

            return Response;
        }

        public abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);

        public static ValidationFailure BuildError(string message)
        {
            return new ValidationFailure(string.Empty, message);
        }
    }
}
