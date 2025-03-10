namespace HotelRoomManager.Application.Queries.GetRooms;

using HotelRoomManager.Application.Interfaces;
using HotelRoomManager.Domain.Enums;

public record GetRoomsQuery(string? Name, RoomSize? Size, bool? Availability) : IQuery;