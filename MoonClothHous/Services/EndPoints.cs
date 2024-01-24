using System;
namespace MoonClothHous.Services
{
	public class EndPoints
	{
		public static string BaseURL = "http://localhost:7242";


        public static string productsImageData = "/api/productImageData";
		public static string Login = "/api/customer/login";
		public static string productById = "/api/products/{id}";
		public static string productImageById = "/api/productImageData/{id}";

    }
}

