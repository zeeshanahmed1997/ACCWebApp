using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Clothing
{
    public int ClothingId { get; set; }

    public string? ClothingName { get; set; }

    public decimal? Price { get; set; }

    public int? GenderId { get; set; }

    public int? TypeId { get; set; }

    public int? FabricId { get; set; }

    public virtual Fabric? Fabric { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual ClothingType? Type { get; set; }
}
