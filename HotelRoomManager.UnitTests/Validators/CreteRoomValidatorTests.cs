namespace HotelRoomManager.UnitTests.Validators;

using FluentAssertions;
using HotelRoomManager.Application.DTOs.Requests;
using HotelRoomManager.Application.Validators;
using Xunit;

public class CreteRoomValidatorTests
{
    private readonly CreateRoomValidator _sut = new();

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenNoFilterApplied()
    {
        // Assign
        var dto = new CreateRoomRequestDto()
        {
            Name = "RoomName"
        };

        // Act
        var result = _sut.Validate(dto);

        // Arrange
        result.IsValid.Should().Be(true);
        result.Message.Should().BeNull();
    }
}