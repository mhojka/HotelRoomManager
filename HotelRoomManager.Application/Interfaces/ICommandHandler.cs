namespace HotelRoomManager.Application.Interfaces;

public interface ICommandHandler<in TCommand, out TResponseDto> where TCommand : ICommand
{
    TResponseDto Handle(TCommand command);
}