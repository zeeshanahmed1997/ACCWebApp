using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Clothing
{
    public int ClothingId { get; set; }

    public string? ClothingName { get; set; }

    public decimal? Price { get; set; }

    public int? GenderId { get; set; }

    public int? TypeId { get; set; }

    public int? FabricId { get; set; }

    [JsonIgnore]
    public  Fabric? Fabric { get; set; }

    [JsonIgnore]
    public  Gender? Gender { get; set; }

    [JsonIgnore]
    public  ClothingType? Type { get; set; }
}
