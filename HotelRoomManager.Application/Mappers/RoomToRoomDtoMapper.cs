namespace HotelRoomManager.Application.Mappers;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Domain.Entities;

public class RoomToRoomDtoMapper : IMapper<Room, RoomDto>
{
    public RoomDto Map(Room from)
    {
        return new RoomDto
        {
            Number = from.Number,
            Id = from.Id,
            Size = from.Size,
            IsAvailable = from.OccupiedType is null,
            OccupiedType = from.OccupiedType,
            OccupiedDescription = from.OccupiedDescription
        };
    }
}