using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Domain.Models;

[DataContract]
public partial class ClothingType
{

    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    [JsonIgnore]
    public  ICollection<Clothing> Clothings { get; set; } = new List<Clothing>();
}
