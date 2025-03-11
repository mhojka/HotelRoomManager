namespace HotelRoomManager.Infractructure.Repositories;

using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Domain.Enums;
using HotelRoomManager.Infractructure.Context;
using Microsoft.EntityFrameworkCore;

public class RoomRepository : IRoomRepository
{
    private readonly HotelRoomContext _context;

    public RoomRepository(HotelRoomContext context)
    {
        _context = context;
    }

    public async Task<List<Room>> GetAll(string? number, RoomSize? size, bool? availability)
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

        return await rooms.ToListAsync();
    }

    public async Task<Room?> GetById(int id)
    {
        return await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(Room room)
    {
        _context.Rooms.Update(room);
        await _context.SaveChangesAsync();
    }

    public async Task Create(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
    }
}