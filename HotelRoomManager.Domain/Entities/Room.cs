namespace HotelRoomManager.Domain.Entities
{
    using HotelRoomManager.Domain.Enums;

    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public RoomSize Size { get; set; }

        public OccupiedType? OccupiedType { get; set; }

        public string? OccupiedDescription { get; set; }
    }
}
