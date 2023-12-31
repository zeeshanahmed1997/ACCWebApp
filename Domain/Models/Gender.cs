﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain.Models.MoonClothHouse;

namespace Domain.Models;
public partial class Gender
{
    public int GenderId { get; set; }

    public string? GenderName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Clothing> Clothings { get; set; } = new List<Clothing>();

    [JsonIgnore]
    public virtual ICollection<Sales> Sales { get; set; } = new List<Sales>();

}
