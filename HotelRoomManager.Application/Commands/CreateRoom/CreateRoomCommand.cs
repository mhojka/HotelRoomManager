namespace HotelRoomManager.Application.Commands.CreateRoom;

using HotelRoomManager.Application.DTOs.Requests;
using HotelRoomManager.Application.Interfaces;

public record CreateRoomCommand(CreateRoomRequestDto CreateRoomRequestDto) : ICommand;