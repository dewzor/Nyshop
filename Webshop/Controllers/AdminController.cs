using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Webshop.Data;
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
             
            StoreManager manage = new StoreManager();
            EditProduct editproduct = manage.ToEditProduct(product);
            
            var categories = await _store.GetCategoriesAsync();
            //ViewBag.Categories = new SelectList(categories.Select(i => i.Name).Distinct().ToList());
            var list = new List<SelectListItem>();
            if (categories != null)
            {
                foreach (var category in categories)
                {
                    if (category.Name == editproduct.Category.Name)
                    {
                        list.Add(new SelectListItem()
                        {Text = category.Name,Value = category.CategoryId.ToString(), Selected = true});
                    }
                    else 
                    list.Add(new SelectListItem(){Text = category.Name, Value = category.CategoryId.ToString()});
                }
            }
            ViewBag.Categories = list;

            return View(editproduct);
        }

        [HttpPost]
        public JsonResult CategoryList()
        {
            // normally, you pass a list obtained from ORM or ADO.NET DataTable or DataReader
            var categories =  _store.GetCategories();
            string json = new JavaScriptSerializer().Serialize(categories);
            //return Json(new Dictionary<string, string>() { { "PH", "Philippines" }, { "CN", "China" }, { "CA", "Canada" }, { "JP", "Japan" } }.ToList());
            var result = Json(json, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public JsonResult AddCategory(string jsonData)
        {
            var hej = jsonData;


            var categories = _store.GetCategories();
            string json = new JavaScriptSerializer().Serialize(categories);
            //return Json(new Dictionary<string, string>() { { "PH", "Philippines" }, { "CN", "China" }, { "CA", "Canada" }, { "JP", "Japan" } }.ToList());
            var result = Json(json, JsonRequestBehavior.AllowGet);
            return result;
        }

        public async Task<ActionResult> AddProduct()
        {
            var categories = await _store.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Select(i => i.Name).Distinct().ToList());
            return View();
        }
    }
}