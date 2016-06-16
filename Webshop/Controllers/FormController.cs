using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Webshop.Data;
using Webshop.Logic;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class FormController : Controller
    {
        private StoreManager _manage = new StoreManager();
        // GET: Form
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            _manage.UpdateProduct(product);
            return RedirectToAction("Details", "Store", new { id = product.ProductId });
        }

        [HttpPost]
        public ActionResult UploadImg(EditProduct file)  //Skall vara actionresult inte void.
        {
            
            FormLogic check = new FormLogic();
            var result = check.IsImage(file.ProfileImage); //Checks thats the uploaded file is an image.
            string extension = Path.GetExtension(file.ProfileImage.FileName); //Stores files extension for saving correct filetype.
            if (result && file.ProfileImage.ContentLength < 3000000) //If its an image AND its under 3mb, upload it.
            {
                string path = Server.MapPath("~/Images/Products/" + file.ProductId + "/" + file.Name + extension);
                if(!Directory.Exists("~/Images/Products/" + file.ProductId + "/"))
                    Directory.CreateDirectory("~/Images/Products/" + file.ProductId);
                
                file.ProfileImage.SaveAs(path);
                file.ImageUrl = "~/Images/Products/" + file.ProductId + "/" + file.Name + extension;
                _manage.UpdateProductImage(file);
            }
            
            return RedirectToAction("EditProduct", "Admin", new { id = file.ProductId});
        }
    }
}