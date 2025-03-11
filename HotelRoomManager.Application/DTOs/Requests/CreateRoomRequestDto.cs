namespace HotelRoomManager.Application.DTOs.Requests;

using HotelRoomManager.Domain.Enums;

public class CreateRoomRequestDto
{
    public required string Number { get; set; }

    public RoomSize Size { get; set; }
}