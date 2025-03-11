namespace HotelRoomManager.Application.DTOs;

using HotelRoomManager.Domain.Enums;

public class RoomDto
{
    public int Id { get; set; }

    public required string Number { get; set; }

    public required RoomSize Size { get; set; }

    public bool IsAvailable { get; set; }

    public OccupiedType? OccupiedType { get; set; }

    public string? OccupiedDescription { get; set; }
}