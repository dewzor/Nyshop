using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class FormController : Controller
    {
        // GET: Form
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void EditProduct(Models.Product product)
        {
            var x = product;

        }

        [HttpPost]
        public ActionResult UploadImg(EditProduct file)  //Skall vara actionresult inte void.
        {
            string path = Server.MapPath("~/Images/Products/" + file.ProfileImage.FileName);
            file.ProfileImage.SaveAs(path);
            return RedirectToAction("EditProduct", "Admin");
        }
    }
}