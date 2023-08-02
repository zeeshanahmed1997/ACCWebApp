using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;
public partial class Fabric
{
    public int FabricId { get; set; }

    public string? FabricName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Clothing> Clothings { get; set; } = new List<Clothing>();

}
