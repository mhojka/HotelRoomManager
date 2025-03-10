namespace HotelRoomManager.Application.DTOs;

using HotelRoomManager.Domain.Enums;

public class RoomDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required RoomSize Size { get; set; }

    public bool Availability { get; set; }

    public OccupiedType? OccupiedType { get; set; }

    public string? OccupiedDescription { get; set; }
}