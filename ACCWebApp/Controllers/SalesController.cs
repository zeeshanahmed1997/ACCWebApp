using ACCWebApp.Models;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCWebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SalesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: SalesController
        public ActionResult getSales()
        {
            return View();
        }

        // GET: SalesController/Details/5
        public ActionResult DetailsofSale(int id)
        {
            return View();
        }

        // GET: SalesController/Create
        public async Task<ActionResult> CreateSales()
        {
            var model = new SalesViewModel();

            // Fetch fabric data
            model.Fabrics = await FetchFabrics();

            // Fetch gender data
            model.Genders = await FetchGenders();

            // Fetch clothing type data
            model.ClothingTypes = await FetchClothingTypes();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SalesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var salesDocument = new SalesDocument
                {
                    FabricId = model.FabricId,
                    GenderId = model.GenderId,
                    TypeId = model.TypeId,
                    CustomerName = model.CustomerName,
                    SaleDate = model.SaleDate,
                    TotalAmount = model.TotalAmount
                    // Assign other properties of the sales document
                };

                var httpClient = _httpClientFactory.CreateClient();

                var response = await httpClient.PostAsJsonAsync("https://localhost:7241/api/sales", salesDocument);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the sales document.");
                }
            }

            // Fetch fabric data
            model.Fabrics = await FetchFabrics();

            // Fetch gender data
            model.Genders = await FetchGenders();

            // Fetch clothing type data
            model.ClothingTypes = await FetchClothingTypes();

            return View(model);
        }

        private async Task<List<Fabric>> FetchFabrics()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7241/api/Fabric");

            if (response.IsSuccessStatusCode)
            {
                var fabrics = await response.Content.ReadAsAsync<List<Fabric>>();
                return fabrics;
            }

            return new List<Fabric>();
        }

        private async Task<List<Gender>> FetchGenders()
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

        private async Task<List<ClothingType>> FetchClothingTypes()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7241/api/clothingType");

            if (response.IsSuccessStatusCode)
            {
                var clothingTypes = await response.Content.ReadAsAsync<List<ClothingType>>();
                return clothingTypes;
            }

            return new List<ClothingType>();
        }


        // GET: SalesController/Edit/5
        public ActionResult EditSales(int id)
        {
            return View();
        }

        // POST: SalesController/Edit/5
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

        // GET: SalesController/Delete/5
        public ActionResult DeleteSales(int id)
        {
            return View();
        }

        // POST: SalesController/Delete/5
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
