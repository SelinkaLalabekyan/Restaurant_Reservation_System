using System;
using System.Collections.Generic;

namespace RestaurantApi.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
