namespace HotelRoomManager.Application.Services;

using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.DTOs.Requests;
using HotelRoomManager.Domain.Enums;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllRooms(string name, RoomSize size);
    Task<RoomDto> CreateRoom(CreateRoomRequestDto createRoomDto);
    Task<RoomDto> UpdateRoomAsync(Guid id, UpdateRoomRequestDto updateRoomDto);
    Task<RoomDto> MarkRoomAsOccupied(Guid id, string? details = null);
    Task<RoomDto> MarkRoomAsCleaning(Guid id, string? details = null);
    Task<RoomDto> MarkRoomAsMaintenance(Guid id, string details);
    Task<RoomDto> MarkRoomAsManual(Guid id, string details);
    Task<RoomDto> MarkRoomAsAvailable(Guid id);

}