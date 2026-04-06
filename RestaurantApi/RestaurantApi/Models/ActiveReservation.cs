using System;
using System.Collections.Generic;

namespace RestaurantApi.Models;

public partial class ActiveReservation
{
    public int ReservationId { get; set; }

    public string? Name { get; set; }

    public int TableId { get; set; }

    public DateOnly? ReservationDate { get; set; }
}
