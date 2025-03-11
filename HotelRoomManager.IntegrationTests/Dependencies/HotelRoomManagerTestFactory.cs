namespace HotelRoomManager.IntegrationTests.Dependencies;

using HotelRoomManager.Domain.Entities;
using HotelRoomManager.Domain.Enums;
using HotelRoomManager.Infractructure.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class HotelRoomManagerTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await FillSampleData();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return Task.CompletedTask;
    }

    private static async Task FillSampleData()
    {
        var options = new DbContextOptionsBuilder<HotelRoomContext>()
            .UseInMemoryDatabase("InMemoryDb").Options;
        await using var context = new HotelRoomContext(options);

        await context.Rooms.AddAsync(new Room
        {
            Number = "001",
            Size = RoomSize.Single
        });

        await context.Rooms.AddAsync(new Room
        {
            Number = "002",
            Size = RoomSize.Single
        });

        await context.SaveChangesAsync();
    }
}