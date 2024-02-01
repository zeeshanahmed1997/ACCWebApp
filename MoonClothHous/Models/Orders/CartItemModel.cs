using System;
namespace MoonClothHous.Models.Orders
{
	public class CartItemModel
	{
		public string CartItemId { get; set; }
		public string CartId { get; set; }
		public string ProductId { get; set; }
		public int Quantity { get; set; }
		public double PricePerUnit { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
    }
}

