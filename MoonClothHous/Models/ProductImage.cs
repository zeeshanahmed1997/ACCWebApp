using System.Text.Json.Serialization;

namespace MoonClothHous.Models
{
    public partial class ProductImage
    {
        [JsonPropertyName("imageId")]
        public string ImageId { get; set; } = null!;

        [JsonPropertyName("productId")]
        public string? ProductId { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = null!;

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

}
