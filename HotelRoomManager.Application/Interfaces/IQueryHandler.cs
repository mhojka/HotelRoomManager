namespace HotelRoomManager.Application.Interfaces;

public interface IQueryHandler<in TQuery, out TResponse> where TQuery : IQuery
{
    TResponse Handle(TQuery query);
}