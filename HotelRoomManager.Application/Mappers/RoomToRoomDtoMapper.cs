namespace HotelRoomManager.Application.Mappers;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Domain.Entities;

public class RoomToRoomDtoMapper : IMapper<Room, RoomDto>
{
    public RoomDto Map(Room from)
    {
        return new RoomDto
        {
            Name = from.Name,
            Id = from.Id,
            Size = from.Size,
            Availability = from.OccupiedType is null,
            OccupiedType = from.OccupiedType,
            OccupiedDescription = from.OccupiedDescription
        };
    }
}