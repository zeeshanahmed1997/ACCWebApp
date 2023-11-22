using ACCWebApp.Models;
using Domain.Models;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACCWebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SalesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sales/Create
        public async Task<ActionResult> Create()
        {
            var model = new SalesViewModel();


            // Fetch gender data
            model.Genders = await FetchGenders();

            // Fetch clothing type data
            model.Clothings = await FetchClothings();

            return View(model);
        }

        // POST: Sales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SalesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sale = new Sales
                {
                    //SaleId = 0, // Commented out, assuming it's auto-generated
                    GenderId = model.GenderId,
                    ClothingId = model.ClothingId,
                    Description = model.Description,
                    ActualPrice = model.ActualPrice,
                    SalePrice = model.SalePrice,
                    Date = model.SaleDate, // Assuming 'Date' property corresponds to 'SaleDate' in the model
                    Quantity = model.Quantity,
                    // Assign other properties of the sale
                };

                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsJsonAsync("https://localhost:7241/api/sales", sale);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("SalesDashboard", "Home");

                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "An error occurred while creating the sales document.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the sales document: " + ex.Message);
                }
            }

            // Fetch gender data
            model.Genders = await FetchGenders();

            return RedirectToAction("SalesDashboard", "Home");

        }


        [HttpGet]
        public async Task<List<Clothing>> FetchClothings()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7241/api/clothes");

            if (response.IsSuccessStatusCode)
            {
                var fabrics = await response.Content.ReadAsAsync<List<Clothing>>();
                return fabrics;
            }

            return new List<Clothing>();
        }
        [HttpGet]
        public async Task<List<Gender>> FetchGenders()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7241/api/Gender");

            if (response.IsSuccessStatusCode)
            {
                var genders = await response.Content.ReadAsAsync<List<Gender>>();
                return genders;
            }

            return new List<Gender>();
        }


        // GET: Sales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sales/Edit/5
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

        // GET: Sales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sales/Delete/5
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
