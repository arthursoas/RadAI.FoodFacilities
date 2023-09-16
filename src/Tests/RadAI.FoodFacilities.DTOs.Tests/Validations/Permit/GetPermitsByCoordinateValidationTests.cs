using RadAI.FoodFacilities.DTOs.Requests.Permit;
using Shouldly;

namespace RadAI.FoodFacilities.DTOs.Tests.Validations.Permit
{
    public class GetPermitsByCoordinateValidationTests
    {
        [Fact]
        public void Validate_WhenRequestIsValid_ShouldReturnNoErrors()
        {
            // Arrange
            var request = new GetPermitsByCoordinateRequest
            {
                Latitude = 0,
                Longitude = 0,
                Status = "APPROVED",
                FacilityType = "TRUCK"
            };

            // Act
            var result = request.Validate();

            // Assert
            result.IsValid.ShouldBeTrue();
            result.Errors.ShouldBeEmpty();
        }

        [Theory]
        [InlineData(-91)]
        [InlineData(91)]
        [InlineData(null)]
        public void Validate_WhenLatitudeIsInvalid_ShouldReturnErrors(double? latitude)
        {
            // Arrange
            var request = new GetPermitsByCoordinateRequest
            {
                Latitude = latitude,
                Longitude = 0,
                Status = "APPROVED",
                FacilityType = "TRUCK"
            };

            // Act
            var result = request.Validate();

            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldNotBeEmpty();
        }

        [Theory]
        [InlineData(-181)]
        [InlineData(181)]
        [InlineData(null)]
        public void Validate_WhenLongitudeIsInvalid_ShouldReturnErrors(double? longitude)
        {
            // Arrange
            var request = new GetPermitsByCoordinateRequest
            {
                Latitude = 0,
                Longitude = longitude,
                Status = "APPROVED",
                FacilityType = "TRUCK"
            };

            // Act
            var result = request.Validate();

            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldNotBeEmpty();
        }
    }
}
