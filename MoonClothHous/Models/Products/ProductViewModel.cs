using System.Text.Json.Serialization;

namespace MoonClothHous.Models.Products
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Color { get; set; }
    }
}