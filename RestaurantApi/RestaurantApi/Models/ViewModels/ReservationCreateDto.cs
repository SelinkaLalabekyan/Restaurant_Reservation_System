namespace RestaurantApi.Models.ViewModels
{
    public class ReservationCreateDto
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }

        public DateTime Date { get; set; }

        public int GuestCount { get; set; }
    }
}