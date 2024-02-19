using System;
namespace Domain.Models.ResponseModels
{
    public class ProductImageResponse
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ImageBase64 { get; set; }
        public string ContentType { get; set; }
    }
}

