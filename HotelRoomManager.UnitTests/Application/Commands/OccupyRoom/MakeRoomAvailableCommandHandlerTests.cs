using HotelRoomManager.Application.Commands.MakeRoomAvailable;
using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.Mappers;
using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Domain.Enums;
using HotelRoomManager.Infractructure.Repositories;
using System.Net;
using Xunit;

namespace HotelRoomManager.UnitTests.Application.Commands.OccupyRoom;

using NSubstitute;

public class MakeRoomAvailableCommandHandlerTests
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomDto> _roomMapper;
    private readonly MakeRoomAvailableCommandHandler _sut;

    public MakeRoomAvailableCommandHandlerTests()
    {
        _roomRepository = Substitute.For<IRoomRepository>();
        _roomMapper = Substitute.For<IMapper<Room, RoomDto>>();

        _sut = new MakeRoomAvailableCommandHandler(_roomRepository, _roomMapper);
    }

    [Fact]
    public async Task Handle_WithExistingRoom_ShouldMarkRoomAsAvailableAndReturnOkResponse()
    {
        // Arrange
        var roomId = 1;
        var command = new MakeRoomAvailableCommand(roomId);

        var room = new Room
        {
            Id = roomId,
            Number = "101",
            Size = RoomSize.Double,
            OccupiedType = OccupiedType.Booked,
            OccupiedDescription = null
        };

        var expectedDto = new RoomDto
        {
            Id = roomId,
            Number = "101",
            Size = RoomSize.Double,
            IsAvailable = true,
            OccupiedType = null,
            OccupiedDescription = null
        };

        _roomRepository.GetById(roomId).Returns(room);
        _roomMapper.Map(Arg.Any<Room>()).Returns(expectedDto);

        // Act
        var result = await _sut.Handle(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(expectedDto, result.Data);
        Assert.Null(room.OccupiedType);
        Assert.Null(room.OccupiedDescription);

        await _roomRepository.Received(1).Update(room);

        _roomMapper.Received(1).Map(room);
    }

    [Fact]
    public async Task Handle_WithExistingRoomUnderMaintenance_ShouldClearDescriptionAndReturnOkResponse()
    {
        // Arrange
        var roomId = 2;
        var command = new MakeRoomAvailableCommand(roomId);

        var room = new Room
        {
            Id = roomId,
            Number = "102",
            Size = RoomSize.Suite,
            OccupiedType = OccupiedType.Maintenance,
            OccupiedDescription = "Fixing leaky faucet"
        };

        var expectedDto = new RoomDto
        {
            Id = roomId,
            Number = "102",
            Size = RoomSize.Suite,
            IsAvailable = true,
            OccupiedType = null,
            OccupiedDescription = null
        };

        _roomRepository.GetById(roomId).Returns(room);
        _roomMapper.Map(Arg.Any<Room>()).Returns(expectedDto);

        // Act
        var result = await _sut.Handle(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(expectedDto, result.Data);
        Assert.Null(room.OccupiedType);
        Assert.Null(room.OccupiedDescription);
        await _roomRepository.Received(1).Update(room);
    }

    [Fact]
    public async Task Handle_WithNonExistentRoom_ShouldReturnNotFoundResponse()
    {
        // Arrange
        var roomId = 999; // Non-existent room ID
        var command = new MakeRoomAvailableCommand(roomId);

        _roomRepository.GetById(roomId).Returns((Room)null!);

        // Act
        var result = await _sut.Handle(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.Null(result.Data);
        Assert.Equal($"Room with Id {roomId} not found", result.Message);
        await _roomRepository.Received(1).GetById(roomId);
        await _roomRepository.DidNotReceive().Update(Arg.Any<Room>());
        _roomMapper.DidNotReceive().Map(Arg.Any<Room>());
    }

    [Fact]
    public async Task Handle_WithAlreadyAvailableRoom_ShouldStillUpdateAndReturnOkResponse()
    {
        // Arrange
        var roomId = 3;
        var command = new MakeRoomAvailableCommand(roomId);

        var room = new Room
        {
            Id = roomId,
            Number = "103",
            Size = RoomSize.Single,
            OccupiedType = null,
            OccupiedDescription = null
        };

        var expectedDto = new RoomDto
        {
            Id = roomId,
            Number = "103",
            Size = RoomSize.Single,
            IsAvailable = true,
            OccupiedType = null,
            OccupiedDescription = null
        };

        _roomRepository.GetById(roomId).Returns(room);
        _roomMapper.Map(Arg.Any<Room>()).Returns(expectedDto);

        // Act
        var result = await _sut.Handle(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(expectedDto, result.Data);

        await _roomRepository.Received(1).Update(room);
        _roomMapper.Received(1).Map(room);
    }
}