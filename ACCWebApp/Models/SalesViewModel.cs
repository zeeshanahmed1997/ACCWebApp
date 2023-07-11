using Domain.Models;
using System;
using System.Collections.Generic;

public class SalesViewModel
{
    public int FabricId { get; set; }
    public int GenderId { get; set; }
    public int TypeId { get; set; }
    public string Description { get; set; }
    public decimal ActualPrice { get; set; }
    public decimal SalePrice { get; set; }
    public DateTime SaleDate { get; set; }
    public TimeSpan SaleTime { get; set; }

    public List<Fabric> Fabrics { get; set; }
    public List<Gender> Genders { get; set; }
    public List<ClothingType> ClothingTypes { get; set; }
}
