using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class CartItem
{
    public string CartItemId { get; set; } = null!;

    public string? CartId { get; set; }

    public string? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal PricePerUnit { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Product? Product { get; set; }
}
