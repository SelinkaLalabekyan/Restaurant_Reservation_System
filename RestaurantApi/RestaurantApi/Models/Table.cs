using System;
using System.Collections.Generic;

namespace RestaurantApi.Models;

public partial class Table
{
    public int TableId { get; set; }

    public int Capacity { get; set; }

    public int? AreaId { get; set; }

    public virtual RestaurantArea? Area { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
