using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webshop.Services;

namespace Webshop.Controllers
{
    public class EmployeeController : Controller
    {
        private StoreService _store;
        public EmployeeController() : this(new StoreService()) { }
        public EmployeeController(StoreService service)
        {
            _store = service;
        }
        // GET: Employee
        [Authorize(Roles = "Admin, Employee")]
        public async Task<ActionResult> Index()
        {
            var categories = await _store.GetCategoriesAsync();
            var products = await _store.GetAllProducts();
            ViewBag.Products = new SelectList(products.Select(i => i.Name).Distinct().ToList());
            return View(categories);
        }


    }
}