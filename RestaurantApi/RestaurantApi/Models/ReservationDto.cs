namespace RestaurantApi.Models
{
    public class ReservationDto
    {   public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int GuestCount { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<ReservationDto> Reservations { get; set; }
    }
}
