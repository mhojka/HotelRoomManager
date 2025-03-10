namespace HotelRoomManager.Application.Commands.OccupyRoom;

using HotelRoomManager.Application.DTOs.Requests;
using HotelRoomManager.Application.Interfaces;

public record OccupyRoomCommand(int Id, OccupyRequestDto UpdateRoomRequestDto) : ICommand;