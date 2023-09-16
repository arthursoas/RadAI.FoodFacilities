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
    public class GetPermitsByApplicantHandlerTests
    {
        private readonly IPermitManager _permitManager;
        private readonly IRequestHandler<GetPermitsByApplicantRequest, ResponseBase<GetPermitResponse[]>> _handler;

        public GetPermitsByApplicantHandlerTests()
        {
            _permitManager = Substitute.For<IPermitManager>();

            _handler = new GetPermitsByApplicantHandler(
                _permitManager);
        }

        [Fact]
        public async Task Handle_WhenRequestIsInvalid_ShouldReturnValidationErrors()
        {
            // Arrange
            var request = new GetPermitsByApplicantRequest { Applicant = "" };

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
            var request = new GetPermitsByApplicantRequest { Applicant = "Applicant", Status = "Approved" };
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
                    Arg.Is((Expression<Func<PermitEntity, bool>> p) =>
                        p.ToString().Contains("request.Applicant.Equals") &&
                        p.ToString().Contains("request.Status.Equals")),
                    CancellationToken.None);
        }

        [Fact]
        public async Task Handle_WhenRequestDoesntHaveStatusShouldReturnPermits()
        {
            // Arrange
            var request = new GetPermitsByApplicantRequest { Applicant = "Applicant" };
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
                    Arg.Is((Expression<Func<PermitEntity, bool>> p) =>
                        p.ToString().Contains("request.Applicant.Equals") &&
                        !p.ToString().Contains("request.Status.Equals")),
                    CancellationToken.None);
        }
    }
}
