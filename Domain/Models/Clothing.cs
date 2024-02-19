
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain.Models.MoonClothHouse;

namespace Domain.Models;

public partial class Clothing
{
    public int ClothingId { get; set; }

    public string? ClothingName { get; set; }

    public decimal? Price { get; set; }

    public int? GenderId { get; set; }

    public int? TypeId { get; set; }

    public int? FabricId { get; set; }
    public int? Quantity { get; set; }
    [JsonIgnore]
    public virtual Fabric? Fabric { get; set; }
    [JsonIgnore]
    public virtual Gender? Gender { get; set; }
    [JsonIgnore]
    public virtual ICollection<Sales> Sales { get; set; } = new List<Sales>();
    [JsonIgnore]
    public virtual ClothingType? Type { get; set; }
}
