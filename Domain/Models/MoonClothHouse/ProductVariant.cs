using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class ProductVariant
{
    public string VariantId { get; set; } = null!;

    public string? ProductId { get; set; }

    public string VariantName { get; set; } = null!;

    public decimal VariantPrice { get; set; }

    public int VariantStockQuantity { get; set; }

    public string? VariantImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product? Product { get; set; }
}
