namespace HotelRoomManager.IntegrationTests.Tests;

using HotelRoomManager.IntegrationTests.Dependencies;
using Xunit;

public class RoomControllerTests : IClassFixture<HotelRoomManagerTestFactory>
{
    private readonly HotelRoomManagerTestFactory _factory;

    public RoomControllerTests(HotelRoomManagerTestFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAllRooms_ShouldReturnAllRooms_WhenNoFilterApplied()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/rooms");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}