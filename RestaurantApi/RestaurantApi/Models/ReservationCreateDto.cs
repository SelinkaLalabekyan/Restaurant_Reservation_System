public class ReservationCreateDto
{
    public string Name { get; set; }
    public string Phone { get; set; }

    public int TableId { get; set; }

    public DateTime Date { get; set; }

    public int GuestCount { get; set; }

    public int StaffId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Email { get; set; }
}
