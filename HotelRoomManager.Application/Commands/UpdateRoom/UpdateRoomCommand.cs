namespace HotelRoomManager.Application.Commands.UpdateRoom;

using HotelRoomManager.Application.DTOs.Requests;
using HotelRoomManager.Application.Interfaces;

public record UpdateRoomCommand(int Id, UpdateRoomRequestDto UpdateRoomRequestDto) : ICommand;