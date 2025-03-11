namespace HotelRoomManager.Application.Commands.CreateRoom;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.DTOs.Responses;
using HotelRoomManager.Application.Interfaces;
using HotelRoomManager.Application.Mappers;
using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Infractructure.Repositories;

public class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand, ResponseDto<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomDto> _roomToRoomDtoMapper;

    public CreateRoomCommandHandler(
        IRoomRepository roomRepository,
        IMapper<Room, RoomDto> roomToRoomDtoMapper)
    {
        _roomRepository = roomRepository;
        _roomToRoomDtoMapper = roomToRoomDtoMapper;
    }

    public ResponseDto<RoomDto> Handle(CreateRoomCommand command)
    {
        if (string.IsNullOrEmpty(command.CreateRoomRequestDto.Number))
        {
            return ResponseDto<RoomDto>.CreateBadRequest($"{nameof(command.CreateRoomRequestDto.Number)} can not be empty");
        }

        var room = new Room
        {
            Number = command.CreateRoomRequestDto.Number,
            Size = command.CreateRoomRequestDto.Size
        };

        _roomRepository.Create(room);

        return ResponseDto<RoomDto>.CreateOkResponse(_roomToRoomDtoMapper.Map(room));
    }
}