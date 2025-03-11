namespace HotelRoomManager.Application.Queries.GetRooms;

using HotelRoomManager.Application.Interfaces;
using HotelRoomManager.Domain.Enums;

public record GetRoomsQuery(string? Number, RoomSize? Size, bool? Availability) : IQuery;