using System.Text.Json.Serialization;

namespace MoonClothHous.Models.Products
{
    public class ProductViewModel
    {
        public Product product { get; set; }
        public ProductImage productImage { get; set; }
    }
}