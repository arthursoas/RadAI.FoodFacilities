using RadAI.FoodFacilities.DTOs.Requests.Permit;
using Shouldly;

namespace RadAI.FoodFacilities.DTOs.Tests.Validations.Permit
{
    public class GetPermitsByApplicantValidationTests
    {
        [Fact]
        public void Validate_WhenRequestIsValid_ShouldReturnNoErrors()
        {
            // Arrange
            var request = new GetPermitsByApplicantRequest
            {
                Applicant = "STUART LITTLE",
            };

            // Act
            var result = request.Validate();

            // Assert
            result.IsValid.ShouldBeTrue();
            result.Errors.ShouldBeEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenApplicantIsInvalid_ShouldReturnErrors(string applicant)
        {
            // Arrange
            var request = new GetPermitsByApplicantRequest
            {
                Applicant = applicant
            };

            // Act
            var result = request.Validate();

            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldNotBeEmpty();
        }
    }
}
