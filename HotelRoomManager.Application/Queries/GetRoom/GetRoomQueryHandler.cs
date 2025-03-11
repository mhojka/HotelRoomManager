namespace HotelRoomManager.Application.Queries.GetRoom;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.DTOs.Responses;
using HotelRoomManager.Application.Interfaces;
using HotelRoomManager.Application.Mappers;
using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Infractructure.Repositories;

public class GetRoomQueryHandler : IQueryHandler<GetRoomQuery, ResponseDto<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomDto> _roomToRoomDtoMapper;

    public GetRoomQueryHandler(IRoomRepository roomRepository, IMapper<Room, RoomDto> roomToRoomDtoMapper)
    {
        _roomRepository = roomRepository;
        _roomToRoomDtoMapper = roomToRoomDtoMapper;
    }

    public async Task<ResponseDto<RoomDto>> Handle(GetRoomQuery query)
    {
        var room = await _roomRepository.GetById(query.Id);
        if (room is null)
        {
            return ResponseDto<RoomDto>.CreateNotFoundResponse($"Room with Id {query.Id} not found");

        }
        return ResponseDto<RoomDto>.CreateOkResponse(_roomToRoomDtoMapper.Map(room));
    }
}