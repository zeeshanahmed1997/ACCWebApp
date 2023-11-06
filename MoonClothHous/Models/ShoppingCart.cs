// ShoppingCart.cs
using Domain.Models.MoonClothHouse;

namespace MoonClothHous.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public List<Product> Items { get; set; } = new List<Product>();

        public void AddToCart(Product product)
        {
            Items.Add(product);
        }
    }

}