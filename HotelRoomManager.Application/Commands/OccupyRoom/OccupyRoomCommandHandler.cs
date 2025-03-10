namespace HotelRoomManager.Application.Commands.OccupyRoom;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.DTOs.Responses;
using HotelRoomManager.Application.Interfaces;
using HotelRoomManager.Application.Mappers;
using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Domain.Enums;
using HotelRoomManager.Domain.Exceptions;
using HotelRoomManager.Infractructure.Repositories;

public class OccupyRoomCommandHandler : ICommandHandler<OccupyRoomCommand, ResponseDto<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomDto> _roomToRoomDtoMapper;

    public OccupyRoomCommandHandler(
        IRoomRepository roomRepository,
        IMapper<Room, RoomDto> roomToRoomDtoMapper)
    {
        _roomRepository = roomRepository;
        _roomToRoomDtoMapper = roomToRoomDtoMapper;
    }

    public ResponseDto<RoomDto> Handle(OccupyRoomCommand command)
    {
        var room = _roomRepository.GetById(command.Id);
        if (room is null)
        {
            return ResponseDto<RoomDto>.CreateNotFoundResponse($"Room with Id {command.Id} not found");
        }

        try
        {
            switch (command.UpdateRoomRequestDto.OccupiedType)
            {
                case OccupiedType.Booked:
                    room.MarkAsBooked();
                    break;
                case OccupiedType.Cleanup:
                    room.MarkAsUnderCleaning(command.UpdateRoomRequestDto.OccupiedMessage);
                    break;
                case OccupiedType.Maintenance:
                    room.MarkAsUnderMaintenance(command.UpdateRoomRequestDto.OccupiedMessage);
                    break;
                case OccupiedType.Manual:
                    room.MarkManuallyUnavailable(command.UpdateRoomRequestDto.OccupiedMessage);
                    break;
                default:
                    throw new ArgumentException(
                        $"Unsupported occupied type {command.UpdateRoomRequestDto.OccupiedType}");
            }
        }
        catch (DomainException ex)
        {
            return ResponseDto<RoomDto>.CreateBadRequest(ex.Message);

        }

        _roomRepository.Update(room);

        return ResponseDto<RoomDto>.CreateOkResponse(_roomToRoomDtoMapper.Map(room));
    }
}