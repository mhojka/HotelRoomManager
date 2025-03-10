namespace HotelRoomManager.Application.Commands.MakeRoomAvailable;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.DTOs.Responses;
using HotelRoomManager.Application.Interfaces;
using HotelRoomManager.Application.Mappers;
using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Infractructure.Repositories;

public class MakeRoomAvailableCommandHandler : ICommandHandler<MakeRoomAvailableCommand, ResponseDto<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomDto> _roomToRoomDtoMapper;

    public MakeRoomAvailableCommandHandler(
        IRoomRepository roomRepository,
        IMapper<Room, RoomDto> roomToRoomDtoMapper)
    {
        _roomRepository = roomRepository;
        _roomToRoomDtoMapper = roomToRoomDtoMapper;
    }

    public ResponseDto<RoomDto> Handle(MakeRoomAvailableCommand command)
    {
        var room = _roomRepository.GetById(command.Id);
        if (room is null)
        {
            return ResponseDto<RoomDto>.CreateNotFoundResponse($"Room with Id {command.Id} not found");
        }

        room.MarkAsAvailable();

        _roomRepository.Update(room);

        return ResponseDto<RoomDto>.CreateOkResponse(_roomToRoomDtoMapper.Map(room));
    }
}