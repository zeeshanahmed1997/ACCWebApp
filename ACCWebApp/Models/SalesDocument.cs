namespace ACCWebApp.Models
{
    public class SalesDocument
    {
        public int FabricId { get; set; }
        public int GenderId { get; set; }
        public int TypeId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal SalePrice { get; set; }
        // Add other properties as needed
    }



}
