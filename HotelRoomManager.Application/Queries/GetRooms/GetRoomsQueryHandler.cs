namespace HotelRoomManager.Application.Queries.GetRooms;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.Interfaces;
using HotelRoomManager.Application.Mappers;
using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Infractructure.Repositories;

public class GetRoomsQueryHandler : IQueryHandler<GetRoomsQuery, List<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomDto> _roomToRoomDtoMapper;

    public GetRoomsQueryHandler(IRoomRepository roomRepository, IMapper<Room, RoomDto> roomToRoomDtoMapper)
    {
        _roomRepository = roomRepository;
        _roomToRoomDtoMapper = roomToRoomDtoMapper;
    }

    public async Task<List<RoomDto>> Handle(GetRoomsQuery query)
    {
        var rooms = await _roomRepository.GetAll(query.Number, query.Size, query.Availability);

        return rooms.ConvertAll(x => _roomToRoomDtoMapper.Map(x));
    }
}