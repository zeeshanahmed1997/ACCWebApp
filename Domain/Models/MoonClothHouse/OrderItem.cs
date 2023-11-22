using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class OrderItem
{
    public string OrderItemId { get; set; } = null!;

    public string? OrderId { get; set; }

    public string? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal PricePerUnit { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
