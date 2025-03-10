namespace HotelRoomManager.Infractructure.Context;

using HotelRoomManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class HotelRoomContext : DbContext
{
    public HotelRoomContext(DbContextOptions<HotelRoomContext> options) : base(options)
    {
    }

    public DbSet<Room> Rooms { get; set; } = null!;


}