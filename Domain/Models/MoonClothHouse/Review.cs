using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class Review
{
    public string ReviewId { get; set; } = null!;

    public string? ProductId { get; set; }

    public string? CustomerId { get; set; }

    public int Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public bool IsVerified { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
