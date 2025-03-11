using HotelRoomManager.Domain.Enums;

namespace HotelRoomManager.Application.DTOs.Requests;

public class UpdateRoomRequestDto()
{
    public required string Number { get; set; }
    public required RoomSize Size { get; set; }
}