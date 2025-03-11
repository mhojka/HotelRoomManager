using HotelRoomManager.Application.Mappers;
using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Domain.Enums;
using Xunit;

namespace HotelRoomManager.UnitTests.Application.Mappers;

public class RoomToRoomDtoMapperTests
{
    private readonly RoomToRoomDtoMapper _sut;

    public RoomToRoomDtoMapperTests()
    {
        _sut = new RoomToRoomDtoMapper();
    }

    [Fact]
    public void Map_ShouldMapBasicProperties_WhenRoomIsAvailable()
    {
        // Arrange
        var room = new Room
        {
            Id = 42,
            Number = "101",
            Size = RoomSize.Double,
            OccupiedType = null,
            OccupiedDescription = null
        };

        // Act
        var dto = _sut.Map(room);

        // Assert
        Assert.Equal(42, dto.Id);
        Assert.Equal("101", dto.Number);
        Assert.Equal(RoomSize.Double, dto.Size);
        Assert.True(dto.IsAvailable);
        Assert.Null(dto.OccupiedType);
        Assert.Null(dto.OccupiedDescription);
    }

    [Fact]
    public void Map_ShouldMapBasicPropertiesAndOccupiedState_WhenRoomIsBooked()
    {
        // Arrange
        var room = new Room
        {
            Id = 43,
            Number = "102",
            Size = RoomSize.Single,
            OccupiedType = OccupiedType.Booked,
            OccupiedDescription = null
        };

        // Act
        var dto = _sut.Map(room);

        // Assert
        Assert.Equal(43, dto.Id);
        Assert.Equal("102", dto.Number);
        Assert.Equal(RoomSize.Single, dto.Size);
        Assert.False(dto.IsAvailable);
        Assert.Equal(OccupiedType.Booked, dto.OccupiedType);
        Assert.Null(dto.OccupiedDescription);
    }

    [Fact]
    public void Map_ShouldMapBasicPropertiesAndMaintenanceState_WhenRoomIsUnderMaintenance()
    {
        // Arrange
        var maintenanceDetails = "Repairing bathroom";
        var room = new Room
        {
            Id = 44,
            Number = "103",
            Size = RoomSize.Suite,
            OccupiedType = OccupiedType.Maintenance,
            OccupiedDescription = maintenanceDetails
        };

        // Act
        var dto = _sut.Map(room);

        // Assert
        Assert.Equal(44, dto.Id);
        Assert.Equal("103", dto.Number);
        Assert.Equal(RoomSize.Suite, dto.Size);
        Assert.False(dto.IsAvailable);
        Assert.Equal(OccupiedType.Maintenance, dto.OccupiedType);
        Assert.Equal(maintenanceDetails, dto.OccupiedDescription);
    }

    [Fact]
    public void Map_ShouldMapBasicPropertiesAndCleanupState_WhenRoomIsUnderCleaning()
    {
        // Arrange
        var cleaningDetails = "Deep cleaning";
        var room = new Room
        {
            Id = 45,
            Number = "104",
            Size = RoomSize.Single,
            OccupiedType = OccupiedType.Cleanup,
            OccupiedDescription = cleaningDetails
        };

        // Act
        var dto = _sut.Map(room);

        // Assert
        Assert.Equal(45, dto.Id);
        Assert.Equal("104", dto.Number);
        Assert.Equal(RoomSize.Single, dto.Size);
        Assert.False(dto.IsAvailable);
        Assert.Equal(OccupiedType.Cleanup, dto.OccupiedType);
        Assert.Equal(cleaningDetails, dto.OccupiedDescription);
    }

    [Fact]
    public void Map_ShouldMapBasicPropertiesAndManualState_WhenRoomIsManuallyUnavailable()
    {
        // Arrange
        var room = new Room
        {
            Id = 46,
            Number = "105",
            Size = RoomSize.Triple,
            OccupiedType = OccupiedType.Manual,
            OccupiedDescription = null
        };

        // Act
        var dto = _sut.Map(room);

        // Assert
        Assert.Equal(46, dto.Id);
        Assert.Equal("105", dto.Number);
        Assert.Equal(RoomSize.Triple, dto.Size);
        Assert.False(dto.IsAvailable);
        Assert.Equal(OccupiedType.Manual, dto.OccupiedType);
        Assert.Null(dto.OccupiedDescription);
    }

    [Fact]
    public void Map_ShouldHandleNullDescription_WhenRoomOccupiedWithNullDescription()
    {
        // Arrange
        var room = new Room
        {
            Id = 47,
            Number = "106",
            Size = RoomSize.Double,
            OccupiedType = OccupiedType.Cleanup,
            OccupiedDescription = null
        };

        // Act
        var dto = _sut.Map(room);

        // Assert
        Assert.Equal(47, dto.Id);
        Assert.Equal("106", dto.Number);
        Assert.Equal(RoomSize.Double, dto.Size);
        Assert.False(dto.IsAvailable);
        Assert.Equal(OccupiedType.Cleanup, dto.OccupiedType);
        Assert.Null(dto.OccupiedDescription);
    }

    [Fact]
    public void Map_ShouldHandleEmptyDescription_WhenRoomOccupiedWithEmptyDescription()
    {
        // Arrange
        var room = new Room
        {
            Id = 48,
            Number = "107",
            Size = RoomSize.Double,
            OccupiedType = OccupiedType.Cleanup,
            OccupiedDescription = string.Empty
        };

        // Act
        var dto = _sut.Map(room);

        // Assert
        Assert.Equal(48, dto.Id);
        Assert.Equal("107", dto.Number);
        Assert.Equal(RoomSize.Double, dto.Size);
        Assert.False(dto.IsAvailable);
        Assert.Equal(OccupiedType.Cleanup, dto.OccupiedType);
        Assert.Equal(string.Empty, dto.OccupiedDescription);
    }
}