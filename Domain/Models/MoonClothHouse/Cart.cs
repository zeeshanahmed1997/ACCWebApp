using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models.MoonClothHouse;

public partial class Cart
{
    public string CartId { get; set; } = null!;

    public string? CustomerId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [JsonIgnore]
    public virtual Customer? Customer { get; set; }
}
