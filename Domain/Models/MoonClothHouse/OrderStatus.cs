using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class OrderStatus
{
    public string StatusId { get; set; } = null!;

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
