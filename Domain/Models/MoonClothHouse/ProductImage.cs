using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class ProductImage
{
    public string ImageId { get; set; } = null!;

    public string? ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool IsPrimary { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product? Product { get; set; }
}
