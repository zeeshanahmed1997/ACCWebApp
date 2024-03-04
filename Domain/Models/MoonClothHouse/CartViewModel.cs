using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models.MoonClothHouse
{
    public partial class CartViewModel
    {
        public string? CustomerId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        [JsonIgnore]
        public virtual Customer? Customer { get; set; }
    }
}
