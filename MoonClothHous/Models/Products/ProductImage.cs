using System.Text.Json.Serialization;
using Domain.Models.MoonClothHouse;

namespace MoonClothHous.Models.Products
{
    public partial class ProductImage
    {
        [JsonPropertyName("imageId")]
        public string ImageId { get; set; } = null;

        [JsonPropertyName("productId")]
        public string? ProductId { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = null;

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Product? Product { get; set; }
    }

}
