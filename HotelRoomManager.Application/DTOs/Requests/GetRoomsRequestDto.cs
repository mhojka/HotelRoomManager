namespace HotelRoomManager.Application.DTOs.Requests
{
    using HotelRoomManager.Domain.Enums;
    using Microsoft.AspNetCore.Mvc;

    public class GetRoomsRequestDto
    {
        [FromQuery]
        public string? Name { get; set; }

        [FromQuery]
        public RoomSize? Size { get; set; }

        [FromQuery]
        public bool? Availability { get; set; }
    }
}
