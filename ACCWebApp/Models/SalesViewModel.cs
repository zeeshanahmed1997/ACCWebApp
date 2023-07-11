using Domain.Models;
using System;
using System.Collections.Generic;

public class SalesViewModel
{
    //public int FabricId { get; set; }
    public int? ClothingId { get; set; }

    public int? GenderId { get; set; }

    //public int? TypeId { get; set; }

    //public int? FabricId { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public decimal? ActualPrice { get; set; }

    public decimal? SalePrice { get; set; }

    public DateTime? Date { get; set; }

    public List<Clothing> Clothings { get; set; }
    public List<Gender> Genders { get; set; }
}
