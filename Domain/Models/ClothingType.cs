using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class ClothingType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Clothing> Clothings { get; set; } = new List<Clothing>();
}
