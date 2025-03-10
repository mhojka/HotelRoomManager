namespace HotelRoomManager.Application.Queries.GetRooms;

using HotelRoomManager.Application.Interfaces;

public record GetRoomsQuery(string? Name, string? Size, string Availability ) : IQuery;