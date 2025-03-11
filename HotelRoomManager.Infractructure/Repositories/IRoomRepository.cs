namespace HotelRoomManager.Infractructure.Repositories;

using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Domain.Enums;

public interface IRoomRepository
{
    Task<List<Room>> GetAll(string? number, RoomSize? size, bool? availability);
    Task<Room?> GetById(int id);
    Task Update(Room room);
    Task Create(Room room);
}