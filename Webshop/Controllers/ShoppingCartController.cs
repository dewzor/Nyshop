using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webshop.Models;
using Webshop.Services;
using Webshop.ViewModels;

namespace Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public async Task<ActionResult> Index()
        {
            var cart = new ShoppingCart(HttpContext);
            var items = await cart.GetCartItemsAsync();

            return View(new ShoppingCartViewModel
            {
                Items = items,
                Total = CalculateCart(items)
            });
        }

        public async Task<ActionResult> AddToCart(int id)
        {
            var cart = new ShoppingCart(HttpContext);

            await cart.AddAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RemoveFromCart(int id)
        {
            var cart = new ShoppingCart(HttpContext);

            await cart.RemoveAsync(id);

            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cart = new ShoppingCart(HttpContext);

            var result = await cart.CheckoutAsync(model);

            if (result.Succeeded)
            {
                TempData["transactionId"] = result.TransactionId;
                return RedirectToAction("Complete");
            }

            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }

        public ActionResult Complete()
        {
            ViewBag.TransactionId = (string) TempData["transactionId"];
            return View();
        }

        private static decimal CalculateCart(IEnumerable<CartItem> items)
        {
            var total = 0m;
            foreach (var item in items)
            {
                total += (item.Product.Price*item.Count);
            }
            return total;
        }
    }
}