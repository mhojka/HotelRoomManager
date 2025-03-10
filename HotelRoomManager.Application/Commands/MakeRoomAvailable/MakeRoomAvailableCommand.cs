namespace HotelRoomManager.Application.Commands.MakeRoomAvailable;

using HotelRoomManager.Application.Interfaces;

public record MakeRoomAvailableCommand(int Id) : ICommand;