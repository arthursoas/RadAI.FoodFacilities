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
    public class GetPermitsByCoordinateHandlerTests
    {
        private readonly IPermitManager _permitManager;
        private readonly IRequestHandler<GetPermitsByCoordinateRequest, ResponseBase<GetPermitResponse[]>> _handler;

        public GetPermitsByCoordinateHandlerTests()
        {
            _permitManager = Substitute.For<IPermitManager>();

            _handler = new GetPermitsByCoordinateHandler(
                _permitManager);
        }

        [Fact]
        public async Task Handle_WhenRequestIsInvalid_ShouldReturnValidationErrors()
        {
            // Arrange
            var request = new GetPermitsByCoordinateRequest { Latitude = 200, Longitude = 200 };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.ValidationResult.IsValid.ShouldBeFalse();
            response.ValidationResult.Errors.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task Handle_WhenRequestHasStatusShouldReturnPermits()
        {
            // Arrange
            var request = new GetPermitsByCoordinateRequest {
                Latitude = 10,
                Longitude = 10,
                Status = "APPROVED",
                FacilityType = "TRUCK"
            };
            var permits = new List<PermitEntity>
            {
                new PermitEntity
                {
                    Id = "1",
                    Applicant = "Applicant",
                    Address = "Address",
                    Latitude = 10,
                    Longitude = 10
                }
            };

            _permitManager
                .GetPermitsByDistanceAsync(
                    Arg.Any<Expression<Func<PermitEntity, bool>>>(),
                    10, 10,
                    5,
                    CancellationToken.None)
                .Returns(permits);


            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.ValidationResult.Errors.ShouldBeEmpty();
            response.Payload!.Length.ShouldBe(1);

            await _permitManager
                .Received(1)
                .GetPermitsByDistanceAsync(
                    Arg.Is((Expression<Func<PermitEntity, bool>> p) =>
                        p.ToString().Contains("request.Status.Equals") &&
                        p.ToString().Contains("request.FacilityType.Equals")),
                    10, 10,
                    5,
                    CancellationToken.None);
        }
    }
}
