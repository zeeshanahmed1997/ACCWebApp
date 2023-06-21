using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Gender
{
    public int GenderId { get; set; }

    public string? GenderName { get; set; }

    public virtual ICollection<Clothing> Clothings { get; set; } = new List<Clothing>();
}
