using System;
using Domain.Models.MoonClothHouse;
using Newtonsoft.Json;

namespace MoonClothHous.Models.Products
{
    public partial class Product
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? CategoryId { get; set; }
        public string? BrandId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public virtual Brand? Brand { get; set; }

        [JsonIgnore]
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        [JsonIgnore]
        public virtual Category? Category { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [JsonIgnore]
        public virtual ICollection<ProductAnalytic> ProductAnalytics { get; set; } = new List<ProductAnalytic>();

        [JsonIgnore]
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();

        [JsonIgnore]
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        [JsonIgnore]
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [JsonIgnore]
        public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();

        [JsonIgnore]
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }

}

