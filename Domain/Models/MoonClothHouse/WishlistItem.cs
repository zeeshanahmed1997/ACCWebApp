using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class WishlistItem
{
    public string WishlistItemId { get; set; } = null!;

    public string? WishlistId { get; set; }

    public string? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
