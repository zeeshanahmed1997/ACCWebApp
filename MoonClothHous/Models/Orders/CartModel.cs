using System;
using System.Collections.Generic;
using Domain.Models.MoonClothHouse;
using Newtonsoft.Json; // Ensure to include the Newtonsoft.Json namespace

namespace MoonClothHous.Models.Orders
{
    public class CartModel
    {

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        [JsonIgnore]
        public virtual Customer? Customer { get; set; }
    }
}
