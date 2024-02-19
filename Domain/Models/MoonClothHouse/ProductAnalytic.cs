using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class ProductAnalytic
{
    public string AnalyticsId { get; set; } = null!;

    public string? ProductId { get; set; }

    public int ViewsCount { get; set; }

    public int SalesCount { get; set; }

    public int AddToCartCount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product? Product { get; set; }
}
