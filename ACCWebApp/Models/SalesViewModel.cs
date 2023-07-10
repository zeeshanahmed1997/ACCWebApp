using Domain.Models;

public class SalesViewModel
{
    public int FabricId { get; set; }
    public int GenderId { get; set; }
    public int TypeId { get; set; }
    public string CustomerName { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }

    public List<Fabric> Fabrics { get; set; }
    public List<Gender> Genders { get; set; }
    public List<ClothingType> ClothingTypes { get; set; }
}