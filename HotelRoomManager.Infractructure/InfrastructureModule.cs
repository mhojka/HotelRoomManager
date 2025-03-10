namespace HotelRoomManager.Infractructure;

using HotelRoomManager.Infractructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureModule
{
    public static IServiceCollection ConfigureInfrastructureModule(this IServiceCollection services)
    {
        services.AddTransient<IRoomRepository, RoomRepository>();

        return services;
    }
}