namespace HotelRoomManager.Domain.Entities
{
    using HotelRoomManager.Domain.Enums;
    using HotelRoomManager.Domain.Exceptions;

    public class Room
    {
        public int Id { get; set; }

        public required string Number { get; set; }

        public RoomSize Size { get; set; }

        public OccupiedType? OccupiedType { get; set; }

        public string? OccupiedDescription { get; set; }

        public void Update(string Number, RoomSize size)
        {
            this.Number = Number;
            Size = size;
        }

        public void MarkManuallyUnavailable(string? details)
        {
            if (string.IsNullOrWhiteSpace(details))
                throw new DomainException("Details are required when manually locking a room");

            OccupiedType = Enums.OccupiedType.Manual;
            OccupiedDescription = null;
        }

        public void MarkAsBooked()
        {
            OccupiedType = Enums.OccupiedType.Booked;
            OccupiedDescription = null;
        }

        public void MarkAsUnderCleaning(string? details = null)
        {
            OccupiedType = Enums.OccupiedType.Cleanup;
            OccupiedDescription = details;
        }

        public void MarkAsUnderMaintenance(string? details)
        {
            if (string.IsNullOrWhiteSpace(details))
                throw new DomainException("Details are required for maintenance");

            OccupiedType = Enums.OccupiedType.Maintenance;
            OccupiedDescription = details;
        }

        public void MarkAsAvailable()
        {
            OccupiedType = null;
            OccupiedDescription = null;
        }
    }
}
