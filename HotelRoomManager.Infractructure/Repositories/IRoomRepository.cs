namespace HotelRoomManager.Infractructure.Repositories;

using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Domain.Enums;

public interface IRoomRepository
{
    List<Room> GetAll(string? number, RoomSize? size, bool? availability);
    Room? GetById(int id);
    void Update(Room room);
    void Create(Room room);
}