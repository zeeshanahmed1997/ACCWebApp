using System;
using Newtonsoft.Json; // Ensure to include the Newtonsoft.Json namespace

namespace MoonClothHous.Models.Orders
{
    public class CartModel
    {
        [JsonProperty("cartId")]
        public string CartId { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

    }
}
