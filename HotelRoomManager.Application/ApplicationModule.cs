namespace HotelRoomManager.Application;

using HotelRoomManager.Application.Commands.CreateRoom;
using HotelRoomManager.Application.Commands.MakeRoomAvailable;
using HotelRoomManager.Application.Commands.OccupyRoom;
using HotelRoomManager.Application.Commands.UpdateRoom;
using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.DTOs.Responses;
using HotelRoomManager.Application.Interfaces;
using HotelRoomManager.Application.Mappers;
using HotelRoomManager.Application.Queries.GetRoom;
using HotelRoomManager.Application.Queries.GetRooms;
using HotelRoomManager.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationModule
{
    public static IServiceCollection ConfigureApplicationModule(this IServiceCollection services)
    {
        services.AddTransient<IQueryHandler<GetRoomsQuery, List<RoomDto>>, GetRoomsQueryHandler>();
        services.AddTransient<IQueryHandler<GetRoomQuery, ResponseDto<RoomDto>>, GetRoomQueryHandler>();

        services.AddTransient<ICommandHandler<UpdateRoomCommand, ResponseDto<RoomDto>>, UpdateRoomCommandHandler>();
        services.AddTransient<ICommandHandler<CreateRoomCommand, ResponseDto<RoomDto>>, CreateRoomCommandHandler>();
        services.AddTransient<ICommandHandler<OccupyRoomCommand, ResponseDto<RoomDto>>, OccupyRoomCommandHandler>();
        services.AddTransient<ICommandHandler<MakeRoomAvailableCommand, ResponseDto<RoomDto>>, MakeRoomAvailableCommandHandler>();

        services.AddTransient<IMapper<Room, RoomDto>, RoomToRoomDtoMapper>();

        return services;
    }
}