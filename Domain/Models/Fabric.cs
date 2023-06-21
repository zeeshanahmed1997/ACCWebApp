using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Fabric
{
    public int FabricId { get; set; }

    public string? FabricName { get; set; }

    public  ICollection<Clothing> Clothings { get; set; } = new List<Clothing>();
}
