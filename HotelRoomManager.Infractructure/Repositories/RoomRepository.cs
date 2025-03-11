namespace HotelRoomManager.Infractructure.Repositories;

using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Domain.Enums;
using HotelRoomManager.Infractructure.Context;

public class RoomRepository : IRoomRepository
{
    private readonly HotelRoomContext _context;

    public RoomRepository(HotelRoomContext context)
    {
        _context = context;
    }

    public List<Room> GetAll(string? number, RoomSize? size, bool? availability)
    {
        var rooms = _context.Rooms.AsQueryable();
        if (number is not null)
        {
            rooms = rooms.Where(r => r.Number.Contains(number));
        }

        if (size is not null)
        {
            rooms = rooms.Where(x => x.Size == size);
        }

        if (availability is not null)
        {
            rooms = rooms.Where(x => x.OccupiedType == null);
        }

        return rooms.ToList();
    }

    public Room? GetById(int id)
    {
        return _context.Rooms.FirstOrDefault(x => x.Id == id);
    }

    public void Update(Room room)
    {
        _context.Rooms.Update(room);
        _context.SaveChanges();
    }

    public void Create(Room room)
    {
        _context.Rooms.Add(room);
        _context.SaveChanges();
    }
}