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
        private readonly EmployeeService _store;
        public EmployeeController() : this(new EmployeeService()) { }
        public EmployeeController(EmployeeService service)
        {
            _store = service;
        }
        // GET: Employee
        public async Task<ActionResult> Index()
        {
            var categories = await _store.GetCategoriesAsync();
            return View(categories);
        }
    }
}