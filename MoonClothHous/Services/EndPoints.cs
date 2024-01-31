using System;
namespace MoonClothHous.Services
{
	public class EndPoints
	{
		public static string BaseURL = "http://localhost:7240";


        public static string productsImageData = "/api/productImageData";
		public static string Login = "/api/customer/login";
		public static string productById = "/api/products/{id}";
		public static string productImageById = "/api/productImageData/{id}";
		public static string Signup = "/api/customer/signup";
		public static string GetAllCustomers = "/api/customer";

    }
}

