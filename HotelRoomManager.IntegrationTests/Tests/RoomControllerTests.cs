namespace HotelRoomManager.IntegrationTests.Tests;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.DTOs.Requests;
using HotelRoomManager.Domain.Enums;
using HotelRoomManager.IntegrationTests.Dependencies;
using Xunit;

public class RoomControllerTests : IClassFixture<HotelRoomManagerTestFactory>
{
    private readonly HotelRoomManagerTestFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

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
        var rooms = await response.Content.ReadFromJsonAsync<List<RoomDto>>(_jsonOptions);
        Assert.NotNull(rooms);
    }

    [Fact]
    public async Task GetAllRooms_ShouldReturnFilteredRooms_WhenFiltersApplied()
    {
        // Arrange
        var client = _factory.CreateClient();
        const string filterNumber = "Test room";

        // Act
        var response = await client.GetAsync($"/rooms?name={filterNumber}");

        // Assert
        response.EnsureSuccessStatusCode();
        var rooms = await response.Content.ReadFromJsonAsync<List<RoomDto>>(_jsonOptions);
        Assert.NotNull(rooms);
        Assert.All(rooms, room =>
        {
            Assert.Contains(filterNumber, room.Number, StringComparison.OrdinalIgnoreCase);
        });
    }

    [Fact]
    public async Task GetRoomById_ShouldReturnRoom_WhenRoomExists()
    {
        // Arrange
        var client = _factory.CreateClient();
        var roomId = 1;

        // Act
        var response = await client.GetAsync($"/rooms/{roomId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var room = await response.Content.ReadFromJsonAsync<RoomDto>(_jsonOptions);
        Assert.NotNull(room);
        Assert.Equal(roomId, room.Id);
    }

    [Fact]
    public async Task GetRoomById_ShouldReturnNotFound_WhenRoomDoesNotExist()
    {
        // Arrange
        var client = _factory.CreateClient();
        var nonExistentRoomId = 999;

        // Act
        var response = await client.GetAsync($"/rooms/{nonExistentRoomId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateRoom_ShouldCreateAndReturnRoom_WhenDataIsValid()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createRoomRequest = new CreateRoomRequestDto
        {
            Number = "Test Room",
            Size = RoomSize.Single,
        };

        // Act
        var response = await client.PostAsJsonAsync("/rooms", createRoomRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdRoom = await response.Content.ReadFromJsonAsync<RoomDto>(_jsonOptions);
        Assert.NotNull(createdRoom);
        Assert.Equal(createRoomRequest.Number, createdRoom.Number);
        Assert.Equal(createRoomRequest.Size, createdRoom.Size);
        Assert.True(createdRoom.IsAvailable);
    }

    [Fact]
    public async Task CreateRoom_ShouldReturnBadRequest_WhenDataIsInvalid()
    {
        // Arrange
        var client = _factory.CreateClient();
        var invalidRoomRequest = new CreateRoomRequestDto
        {
            Number = "",
            Size = RoomSize.Double
        };

        // Act
        var response = await client.PostAsJsonAsync("/rooms", invalidRoomRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateRoom_ShouldUpdateAndReturnRoom_WhenDataIsValid()
    {
        // Arrange
        var client = _factory.CreateClient();
        const int roomId = 1;
        var updateRoomRequest = new UpdateRoomRequestDto
        {
            Number = "Updated Room Number",
            Size = RoomSize.Double
        };

        // Act
        var response = await client.PutAsJsonAsync($"/rooms/{roomId}", updateRoomRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var updatedRoom = await response.Content.ReadFromJsonAsync<RoomDto>(_jsonOptions);
        Assert.NotNull(updatedRoom);
        Assert.Equal(roomId, updatedRoom.Id);
        Assert.Equal(updateRoomRequest.Number, updatedRoom.Number);
        Assert.Equal(updateRoomRequest.Size, updatedRoom.Size);
    }

    [Fact]
    public async Task UpdateRoom_ShouldReturnNotFound_WhenRoomDoesNotExist()
    {
        // Arrange
        var client = _factory.CreateClient();
        var nonExistentRoomId = 999;
        var updateRoomRequest = new UpdateRoomRequestDto
        {
            Number = "Updated Room Number",
            Size = RoomSize.Triple,
        };

        // Act
        var response = await client.PutAsJsonAsync($"/rooms/{nonExistentRoomId}", updateRoomRequest);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task OccupyRoom_ShouldOccupyAndReturnRoom_WhenRoomIsAvailable()
    {
        // Arrange
        var client = _factory.CreateClient();
        const int roomId = 2;
        var occupyRequest = new OccupyRequestDto
        {
            OccupiedType = OccupiedType.Booked,
            OccupiedMessage = null
        };

        // Act
        var response = await client.PostAsJsonAsync($"/rooms/{roomId}/occupy", occupyRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var occupiedRoom = await response.Content.ReadFromJsonAsync<RoomDto>(_jsonOptions);
        Assert.NotNull(occupiedRoom);
        Assert.Equal(roomId, occupiedRoom.Id);
        Assert.False(occupiedRoom.IsAvailable);
    }

    [Fact]
    public async Task MakeRoomAvailable_ShouldReturnNotFound_WhenRoomDoesNotExist()
    {
        // Arrange
        var client = _factory.CreateClient();
        var nonExistentRoomId = 999;

        // Act
        var response = await client.PostAsync($"/rooms/{nonExistentRoomId}/available", null);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}