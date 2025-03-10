namespace HotelRoomManager.Application.Services;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.DTOs.Requests;
using HotelRoomManager.Domain.Enums;

public class RoomService : IRoomService
{
    public Task<IEnumerable<RoomDto>> GetAllRooms(string name, RoomSize size)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> CreateRoom(CreateRoomRequestDto createRoomDto)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> UpdateRoomAsync(Guid id, UpdateRoomRequestDto updateRoomDto)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> MarkRoomAsOccupied(Guid id, string? details = null)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> MarkRoomAsCleaning(Guid id, string? details = null)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> MarkRoomAsMaintenance(Guid id, string details)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> MarkRoomAsManual(Guid id, string details)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> MarkRoomAsAvailable(Guid id)
    {
        throw new NotImplementedException();
    }
}