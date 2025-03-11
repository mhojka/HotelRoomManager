namespace HotelRoomManager.Application.Interfaces;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery
{
    Task<TResponse> Handle(TQuery query);
}