using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Webshop.Controllers
{
    [Authorize(Roles = "Employee, Admin")]
    public class AdminMenuController : Controller
    {
        // GET: Admin

        public async Task<ActionResult> AdminMenu() // Make this Async task?
        {
            if (User.IsInRole("Admin"))
            {
                return PartialView("AdminMenu"); // Could add a model aswell.
            }
            else if (User.IsInRole("Employee"))
            {
                return PartialView("EmployeeMenu");
            }
            else return null;
        }
    }
}