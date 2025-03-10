namespace HotelRoomManager.Application.Commands;

using HotelRoomManager.Application.DTOs.Requests;
using HotelRoomManager.Application.Interfaces;

public class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand, Guid>
{
    public Guid Handle(CreateRoomCommand command)
    {
        throw new NotImplementedException();
    }
}