namespace HotelRoomManager.Application;

using HotelRoomManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationModule
{
    public static IServiceCollection ConfigureApplicationModule(this IServiceCollection services)
    {
        return services.AddTransient<IRoomService, RoomService>();
    }
}