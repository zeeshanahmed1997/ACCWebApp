using System;
namespace MoonClothHous.Services
{
	public class EndPoints
	{
		public static string BaseURL = "http://localhost:7240";

		//ProductImage
        public static string productImageById = "/api/productImageData/{id}";
        public static string productsImageData = "/api/productImageData";

		//Products
		public static string productById = "/api/products/{id}";

		//Accounts
        public static string Login = "/api/customer/login";
        public static string Signup = "/api/customer/signup";

		//Customers
		public static string GetAllCustomers = "/api/customer";

		//Cart
		public static string GetAllCarts = "/api/cart/AllCarts";
		public static string GetCartById = "/api/cart/{id}";
		public static string AddToCart = "/api/cart/CreateCart";

		//CartItems
		public static string GetAllCartItems = "/api/cartItem/AllCartItems";
		public static string GetCartItemById = "/api/cartItem/{id}";
        public static string AddToCartItem = "/api/cartItem/CreateCartItem";
    }
}

