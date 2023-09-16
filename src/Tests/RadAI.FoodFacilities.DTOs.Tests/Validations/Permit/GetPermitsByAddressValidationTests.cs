using RadAI.FoodFacilities.DTOs.Requests.Permit;
using Shouldly;

namespace RadAI.FoodFacilities.DTOs.Tests.Validations.Permit
{
    public class GetPermitsByAddressValidationTests
    {
        [Fact]
        public void Validate_WhenRequestIsValid_ShouldReturnNoErrors()
        {
            // Arrange
            var request = new GetPermitsByAddressRequest
            {
                Address = "ST HENRIQUE LEITE",
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
        public void Validate_WhenAddressIsInvalid_ShouldReturnErrors(string address)
        {
            // Arrange
            var request = new GetPermitsByAddressRequest
            {
                Address = address
            };

            // Act
            var result = request.Validate();

            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldNotBeEmpty();
        }
    }
}
