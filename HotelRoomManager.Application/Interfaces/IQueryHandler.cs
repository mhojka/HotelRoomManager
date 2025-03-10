namespace HotelRoomManager.Application.Interfaces;

internal interface IQueryHandler<in TQuery, out TResponse> where TQuery : IQuery
{
    TResponse Handle(TQuery query);
}