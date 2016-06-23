using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webshop.Services;

namespace Webshop.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreService _store;
        public StoreController() : this(new StoreService()) { }
        public StoreController(StoreService service)
        {
            _store = service;
        }
        // GET: Store
        public async Task<ActionResult> Index()
        {
            var products = await _store.GetAllProducts();
            return View(products);
        }

        public async Task<ActionResult> Browse(string id)
        {
            var products = await _store.GetProductsForAsync(id);

            if (!products.Any())
            {
                return HttpNotFound();
            }
            return View(products);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return HttpNotFound();
            var product = await _store.GetProductByIDAsync(id.Value);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        public async Task<ActionResult> Categories() 
        {
            var products = await _store.GetAllProducts();
            var categories = await _store.GetCategoriesAsync();
            var showcategories = await _store.GetCategoriesWithProductsAsync();


            return View(categories);
        }

        public async Task<ActionResult> ReviewProduct()
        {
            return View();
        }

    }
}