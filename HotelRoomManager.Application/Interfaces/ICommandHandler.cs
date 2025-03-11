namespace HotelRoomManager.Application.Interfaces;

public interface ICommandHandler<in TCommand, TResponseDto> where TCommand : ICommand
{
    Task<TResponseDto> Handle(TCommand command);
}