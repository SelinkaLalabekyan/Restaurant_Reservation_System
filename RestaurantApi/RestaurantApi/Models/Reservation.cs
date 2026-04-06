using System;
using System.Collections.Generic;

namespace RestaurantApi.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public int? CustomerId { get; set; }

    public int? TableId { get; set; }

    public int? StaffId { get; set; }

    public DateOnly? ReservationDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public int? GuestCount { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual Table? Table { get; set; }

}
