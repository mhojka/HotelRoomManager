namespace HotelRoomManager.Application.Queries.GetRooms;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.Interfaces;

public class GetRoomsQueryHandler : IQueryHandler<GetRoomsQuery, IEnumerable<RoomDto>>
{
    public IEnumerable<RoomDto> Handle(GetRoomsQuery query)
    {
        throw new NotImplementedException();
    }
}