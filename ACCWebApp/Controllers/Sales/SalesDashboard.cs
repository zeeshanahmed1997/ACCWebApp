using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCWebApp.Controllers
{
    public class SalesDashboard : Controller
    {
        // GET: SalesDashboard
        public ActionResult Index()
        {
            return View();
        }

        // GET: SalesDashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesDashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesDashboard/Create
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

        // GET: SalesDashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesDashboard/Edit/5
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

        // GET: SalesDashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesDashboard/Delete/5
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
