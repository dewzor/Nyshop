using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webshop.Models;
using Webshop.Services;

namespace Webshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private StoreService _store;
        public AdminController() : this(new StoreService()) { }
        public AdminController(StoreService service)
        {
            _store = service;
        }
        // GET: Employee
        
        public async Task<ActionResult> Index()
        {
            var categories = await _store.GetCategoriesAsync();
            var products = await _store.GetAllProducts();
            ViewBag.Products = new SelectList(products.Select(i => i.Name).Distinct().ToList());
            return View(categories);
        }

        public ActionResult Employee()
        {
            return View();
        }

        public async Task<ActionResult> EditProduct(int? id)
        {
            if (!id.HasValue)
                return HttpNotFound();
            var product = await _store.GetProductByIDAsync(id.Value);
            if (product == null)
                return HttpNotFound();
            EditProduct editproduct = new EditProduct();
            editproduct.Name = product.Name;
            editproduct.Description = product.Description;
            editproduct.Published = product.Published;
            editproduct.CategoryName = product.CategoryName;
            editproduct.Price = product.Price;
            editproduct.ImageUrl = product.ImageUrl;
            

            var categories = await _store.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Select(i => i.Name).Distinct().ToList());
            
            return View(editproduct);
        }
    }
}