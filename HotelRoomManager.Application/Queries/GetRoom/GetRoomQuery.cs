namespace HotelRoomManager.Application.Queries.GetRoom;

using HotelRoomManager.Application.Interfaces;

public record GetRoomQuery(int Id) : IQuery;