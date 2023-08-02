using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models.MoonClothHouse;
public partial class Sales
{
    public int SaleId { get; set; }

    public int? ClothingId { get; set; }

    public int? GenderId { get; set; }

    //public int? TypeId { get; set; }

    //public int? FabricId { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public decimal? ActualPrice { get; set; }

    public decimal? SalePrice { get; set; }

    public DateTime? Date { get; set; }


    [JsonIgnore]
    public virtual Clothing? Clothing { get; set; }

    //[JsonIgnore]
    //public virtual Fabric? Fabric { get; set; }

    [JsonIgnore]
    public virtual Gender? Gender { get; set; }

    //[JsonIgnore]
    //public virtual ClothingType? Type { get; set; }
}
