using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class ProductAttribute
{
    public string AttributeId { get; set; } = null!;

    public string? ProductId { get; set; }

    public string AttributeName { get; set; } = null!;

    public string AttributeValue { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
