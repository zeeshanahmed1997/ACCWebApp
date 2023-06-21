﻿using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class ClothingType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Clothing> Clothings { get; set; } = new List<Clothing>();
}
