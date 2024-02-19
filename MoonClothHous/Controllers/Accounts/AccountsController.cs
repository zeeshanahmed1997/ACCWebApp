using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoonClothHous.Models.Accounts;
using MoonClothHous.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace MoonClothHous.Controllers.Accounts
{
    public class AccountsController : Controller
    {
        string login = EndPoints.Login;
        string baseURL = EndPoints.BaseURL;
        string signup = EndPoints.Signup;
        string getAll = EndPoints.GetAllCustomers;
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountsController(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(baseURL); // Set the correct base URL for your API
            _webHostEnvironment = webHostEnvironment;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            try
            {
                // Serialize the loginModel to JSON
                var jsonLoginModel = JsonConvert.SerializeObject(loginModel);

                using (var httpClient = new HttpClient())
                {

                    // Set the base address of your API
                    httpClient.BaseAddress = new Uri(baseURL);

                    // Create the HTTP content with JSON data
                    var content = new StringContent(jsonLoginModel, Encoding.UTF8, "application/json");

                    // Send a POST request to the API
                    var response = await httpClient.PostAsync(login, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // API request was successful
                        var apiResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into an object
                        var responseData = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);

                        // Access customer information from the response data
                        var token = responseData?.Token;
                        var customer = responseData?.User;
                        // Create a list of claims to store in the user's identity
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, customer?.FirstName), // Store the user's first name as a claim
                    new Claim(ClaimTypes.Email, customer?.Email)      // Store the user's email as a claim
                };

                        // Create an identity with the claims
                        var identity = new ClaimsIdentity(claims, "custom");

                        // Create a principal with the identity
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.Session.SetInt32("SessionTimeout", 600);
                        HttpContext.Session.SetString("Token", token);
                        HttpContext.Session.SetString("UserName", customer?.FirstName);
                        HttpContext.Session.SetString("UserEmail", customer?.Email);
                        HttpContext.Session.SetString("CustomerId", customer?.CustomerId);
                        // Sign in the user with the principal
                        //await HttpContext.SignInAsync(principal);

                        // Return a structured JSON response with customer information#

                        return Json(new
                        {
                            success = true,
                            message = "Login successful",
                            token,
                            customer
                        });
                        //return RedirectToAction(actionName: "ProductsLandingPage", controllerName: "Products");

                        //return RedirectToAction("ProductsLandingPage", "Products");
                    }
                    else
                    {
                        // API request failed
                        return Json(new { success = false, message = "API request failed with status code: " + response.StatusCode });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }
        public ActionResult Logout(int id)
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("Token");
            TempData["LogoutSuccess"] = true;
            // Redirect to a different page or perform other actions as needed
            return RedirectToAction("ProductsLandingPage", "Products");
        }
        // GET: AccountsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountsController/Create
        public ActionResult Signup()
        {
            return View();
        }

        // POST: AccountsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Signup(SignupViewModel signupViewModel)
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    // Set the base address of your API
                    httpClient.BaseAddress = new Uri(baseURL);

                    // Send a GET request to fetch all customers
                    var getAllResponse = await httpClient.GetAsync(getAll);

                    if (getAllResponse.IsSuccessStatusCode)
                    {
                        var getAllApiResponse = await getAllResponse.Content.ReadAsStringAsync();

                        try
                        {
                            // Deserialize the JSON response into a list of customer objects
                            var allCustomers = JsonConvert.DeserializeObject<List<Customer>>(getAllApiResponse);

                            // Generate a new customer ID
                            var lastCustomerId = allCustomers.LastOrDefault()?.CustomerId ?? "CUST00000";
                            var newCustomerId = GenerateNewCustomerId(lastCustomerId);

                            // Assign the new ID to the signupViewModel
                            signupViewModel.CustomerId = newCustomerId;

                            var jsonSignupViewModel = JsonConvert.SerializeObject(signupViewModel);
                            // Create the HTTP content with JSON data
                            var content = new StringContent(jsonSignupViewModel, Encoding.UTF8, "application/json");

                            // Send a POST request to the API
                            var response = await httpClient.PostAsync(signup, content);

                            if (response.IsSuccessStatusCode)
                            {
                                // API request was successful
                                return Json(new { success = true, message = "User created successfully " + response.StatusCode });
                            }
                            else
                            {
                                // API request failed
                                return Json(new { success = false, message = "API request failed with status code: " + response.StatusCode });
                            }
                        }
                        catch (JsonException ex)
                        {
                            // Handle JSON serialization error
                            return Json(new { success = false, message = "Failed to deserialize JSON response: " + ex.Message });
                        }
                    }
                    else
                    {
                        // Failed to fetch existing customers
                        return Json(new { success = false, message = "Failed to fetch existing customers" });
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        // Helper method to generate a new customer ID
        private string GenerateNewCustomerId(string lastCustomerId)
        {
            // Logic to generate a new customer ID based on the lastCustomerId
            // For example: Increment the lastCustomerId by 1
            // You can customize this logic based on your requirements
            int lastId = int.Parse(lastCustomerId.Substring(4)); // Extract the numeric part of the lastCustomerId
            int newId = lastId + 1;
            return "CUST" + newId.ToString("D5"); // Format the new ID with leading zeros if necessary
        }
        // GET: AccountsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
