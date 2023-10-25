using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoonClothHous.Models.Accounts;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace MoonClothHous.Controllers.Accounts
{
    public class AccountsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountsController(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7241"); // Set the correct base URL for your API
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
                    httpClient.BaseAddress = new Uri("https://localhost:7241");

                    // Define the API endpoint
                    var apiEndpoint = "/api/customer/login"; // Update with your actual API endpoint

                    // Create the HTTP content with JSON data
                    var content = new StringContent(jsonLoginModel, Encoding.UTF8, "application/json");

                    // Send a POST request to the API
                    var response = await httpClient.PostAsync(apiEndpoint, content);

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
                        HttpContext.Session.SetString("UserName", customer?.FirstName);
                        HttpContext.Session.SetString("UserEmail", customer?.Email);

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
