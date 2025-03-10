namespace HotelRoomManager.Application.DTOs.Requests;

using HotelRoomManager.Domain.Enums;

public class CreateRoomRequestDto
{
    public required string Name { get; set; }

    public RoomSize Size { get; set; }
}