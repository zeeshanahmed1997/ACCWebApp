using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class Category
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;
    public string Gender { get; set; } = null!; // Add the new Gender property

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Product> ProductsNavigation { get; set; } = new List<Product>();
}