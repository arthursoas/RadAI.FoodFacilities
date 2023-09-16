using MediatR;
using NSubstitute;
using RadAI.FoodFacilities.DTOs.Requests.Permit;
using RadAI.FoodFacilities.DTOs.Responses;
using RadAI.FoodFacilities.DTOs.Responses.Permit;
using RadAI.FoodFacilities.WebAPI.Handlers.Permit;
using RadAI.FoodFacilities.WebAPI.Managers;
using Shouldly;
using System.Linq.Expressions;
using PermitEntity = RadAI.FoodFacilities.DTOs.Entities.Permit;

namespace RadAI.FoodFacilities.WebAPI.Tests.Handlers.Permit
{
    public class GetPermitsByAddressHandlerTests
    {
        private readonly IPermitManager _permitManager;
        private readonly IRequestHandler<GetPermitsByAddressRequest, ResponseBase<GetPermitResponse[]>> _handler;

        public GetPermitsByAddressHandlerTests()
        {
            _permitManager = Substitute.For<IPermitManager>();

            _handler = new GetPermitsByAddressHandler(
                _permitManager);
        }

        [Fact]
        public async Task Handle_WhenRequestIsInvalid_ShouldReturnValidationErrors()
        {
            // Arrange
            var request = new GetPermitsByAddressRequest { Address = "" };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.ValidationResult.IsValid.ShouldBeFalse();
            response.ValidationResult.Errors.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task Handle_WhenRequestIsValid_ShouldReturnPermits()
        {
            // Arrange
            var request = new GetPermitsByAddressRequest { Address = "ADDRESS" };
            var permits = new List<PermitEntity>
            {
                new PermitEntity
                {
                    Id = "1",
                    Applicant = "Applicant",
                    Address = "Address"
                }
            };

            _permitManager
                .GetPermitsAsync(
                    Arg.Any<Expression<Func<PermitEntity, bool>>>(),
                    CancellationToken.None)
                .Returns(permits);
                    

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.ValidationResult.Errors.ShouldBeEmpty();
            response.Payload!.Length.ShouldBe(1);

            await _permitManager
                .Received(1)
                .GetPermitsAsync(
                    Arg.Is((Expression<Func<PermitEntity, bool>> p) => p.ToString().Contains("p.Address.Contains")),
                    CancellationToken.None);
        }
    }
}
