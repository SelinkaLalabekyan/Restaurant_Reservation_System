using System;
using System.Collections.Generic;

namespace RestaurantApi.Models;

public partial class RestaurantArea
{
    public int AreaId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}
