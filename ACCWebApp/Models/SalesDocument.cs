namespace ACCWebApp.Models
{ 
public class SalesDocument
{
    public int SalesDocumentId { get; set; }
    public int FabricId { get; set; }
    public int GenderId { get; set; }
    public int TypeId { get; set; }
    public string CustomerName { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    // Add other properties as needed
}


}
