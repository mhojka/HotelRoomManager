namespace HotelRoomManager.Application.DTOs;

public class RoomDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int Size { get; set; }

    public bool Availability { get; set; }
}