using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Gender
{
    public int GenderId { get; set; }

    public string? GenderName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Clothing> Clothings { get; set; } = new List<Clothing>();

    [JsonIgnore]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

}
