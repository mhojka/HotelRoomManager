namespace HotelRoomManager.Application.DTOs.Requests;

using HotelRoomManager.Domain.Enums;

public record OccupyRequestDto
{
    public required OccupiedType OccupiedType { get; set; }

    public string? OccupiedMessage { get; set; }
}