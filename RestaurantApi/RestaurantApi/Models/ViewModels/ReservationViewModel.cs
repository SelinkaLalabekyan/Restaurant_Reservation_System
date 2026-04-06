namespace RestaurantApi.Models.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string TableInfo { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }

        public int GuestCount { get; set; }
        public string Status { get; set; }
    }
}