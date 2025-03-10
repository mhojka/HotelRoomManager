namespace HotelRoomManager.Application.DTOs.Requests
{
    using Microsoft.AspNetCore.Mvc;

    public class GetRoomsRequestDto
    {
        [FromQuery]
        public string? Name { get; set; }

        [FromQuery]
        public int? Size { get; set; }

        [FromQuery]
        public string? Availability { get; set; }
    }
}
