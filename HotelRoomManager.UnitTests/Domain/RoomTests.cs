using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Domain.Enums;
using HotelRoomManager.Domain.Exceptions;
using Xunit;

namespace HotelRoomManager.UnitTests.Domain;

public class RoomTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange & Act
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single
        };

        // Assert
        Assert.Equal(1, room.Id);
        Assert.Equal("101", room.Number);
        Assert.Equal(RoomSize.Single, room.Size);
        Assert.Null(room.OccupiedType);
        Assert.Null(room.OccupiedDescription);
    }

    [Fact]
    public void Update_ShouldUpdateNumberAndSize()
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single
        };

        // Act
        room.Update("102", RoomSize.Double);

        // Assert
        Assert.Equal("102", room.Number);
        Assert.Equal(RoomSize.Double, room.Size);
    }

    [Fact]
    public void MarkManuallyUnavailable_WithValidDetails_ShouldMarkRoomAsManuallyUnavailable()
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single
        };
        var details = "VIP reservation";

        // Act
        room.MarkManuallyUnavailable(details);

        // Assert
        Assert.Equal(OccupiedType.Manual, room.OccupiedType);
        Assert.Null(room.OccupiedDescription);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void MarkManuallyUnavailable_WithInvalidDetails_ShouldThrowDomainException(string? details)
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single
        };

        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => room.MarkManuallyUnavailable(details));
        Assert.Equal("Details are required when manually locking a room", exception.Message);
    }

    [Fact]
    public void MarkAsBooked_ShouldMarkRoomAsBooked()
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single,
            OccupiedDescription = "Some description"
        };

        // Act
        room.MarkAsBooked();

        // Assert
        Assert.Equal(OccupiedType.Booked, room.OccupiedType);
        Assert.Null(room.OccupiedDescription);
    }

    [Fact]
    public void MarkAsUnderCleaning_WithoutDetails_ShouldMarkRoomAsUnderCleaning()
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single
        };

        // Act
        room.MarkAsUnderCleaning();

        // Assert
        Assert.Equal(OccupiedType.Cleanup, room.OccupiedType);
        Assert.Null(room.OccupiedDescription);
    }

    [Fact]
    public void MarkAsUnderCleaning_WithDetails_ShouldMarkRoomAsUnderCleaningWithDetails()
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single
        };
        var details = "Deep cleaning required";

        // Act
        room.MarkAsUnderCleaning(details);

        // Assert
        Assert.Equal(OccupiedType.Cleanup, room.OccupiedType);
        Assert.Equal(details, room.OccupiedDescription);
    }

    [Fact]
    public void MarkAsUnderMaintenance_WithValidDetails_ShouldMarkRoomAsUnderMaintenance()
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single
        };
        var details = "Plumbing issue";

        // Act
        room.MarkAsUnderMaintenance(details);

        // Assert
        Assert.Equal(OccupiedType.Maintenance, room.OccupiedType);
        Assert.Equal(details, room.OccupiedDescription);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void MarkAsUnderMaintenance_WithInvalidDetails_ShouldThrowDomainException(string? details)
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single
        };

        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => room.MarkAsUnderMaintenance(details));
        Assert.Equal("Details are required for maintenance", exception.Message);
    }

    [Fact]
    public void MarkAsAvailable_ShouldClearOccupiedTypeAndDescription()
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single,
            OccupiedType = OccupiedType.Maintenance,
            OccupiedDescription = "Fixing AC"
        };

        // Act
        room.MarkAsAvailable();

        // Assert
        Assert.Null(room.OccupiedType);
        Assert.Null(room.OccupiedDescription);
    }

    [Fact]
    public void StateTransitions_FromAvailableToAllStatesAndBackToAvailable()
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Number = "101",
            Size = RoomSize.Single
        };

        // Initially available
        Assert.Null(room.OccupiedType);
        Assert.Null(room.OccupiedDescription);

        // Act & Assert: Mark as booked
        room.MarkAsBooked();
        Assert.Equal(OccupiedType.Booked, room.OccupiedType);
        Assert.Null(room.OccupiedDescription);

        // Act & Assert: Mark as available
        room.MarkAsAvailable();
        Assert.Null(room.OccupiedType);
        Assert.Null(room.OccupiedDescription);

        // Act & Assert: Mark under cleaning
        room.MarkAsUnderCleaning("Standard cleaning");
        Assert.Equal(OccupiedType.Cleanup, room.OccupiedType);
        Assert.Equal("Standard cleaning", room.OccupiedDescription);

        // Act & Assert: Mark as available
        room.MarkAsAvailable();
        Assert.Null(room.OccupiedType);
        Assert.Null(room.OccupiedDescription);

        // Act & Assert: Mark under maintenance
        room.MarkAsUnderMaintenance("Fix shower");
        Assert.Equal(OccupiedType.Maintenance, room.OccupiedType);
        Assert.Equal("Fix shower", room.OccupiedDescription);

        // Act & Assert: Mark as available
        room.MarkAsAvailable();
        Assert.Null(room.OccupiedType);
        Assert.Null(room.OccupiedDescription);

        // Act & Assert: Mark manually unavailable
        room.MarkManuallyUnavailable("VIP coming");
        Assert.Equal(OccupiedType.Manual, room.OccupiedType);
        Assert.Null(room.OccupiedDescription);

        // Act & Assert: Mark as available
        room.MarkAsAvailable();
        Assert.Null(room.OccupiedType);
        Assert.Null(room.OccupiedDescription);
    }
}