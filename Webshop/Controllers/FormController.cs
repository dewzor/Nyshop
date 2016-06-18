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
        public ActionResult PostCategory()
        {
            //dosomething
            return null;
        }

        [HttpPost]
        public ActionResult UploadImg(EditProduct product)  //Skall vara actionresult inte void.
        {
            
            _manage.SaveProductImage(product, product.ProductId);
            //FormLogic check = new FormLogic();
            //var result = check.IsImage(file.ProductImage); //Checks thats the uploaded file is an image.
            //string extension = Path.GetExtension(file.ProductImage.FileName); //Stores files extension for saving correct filetype.
            //if (result && file.ProductImage.ContentLength < 3000000) //If its an image AND its under 3mb, upload it.
            //{
            //    string folderPath = Server.MapPath("~/Images/Products/" + file.ProductId + "/");
            //    string path = Path.Combine(folderPath + file.Name + extension);
            //    if (!Directory.Exists(folderPath))
            //        Directory.CreateDirectory(folderPath);

            //    file.ProductImage.SaveAs(path);
            //    file.ImageUrl = "~/Images/Products/" + file.ProductId + "/" + file.Name + extension;

            //    _manage.UpdateProductImage(file.ProductId, file.ImageUrl);
            //}

            return RedirectToAction("EditProduct", "Admin", new { id = product.ProductId});
        }
        [HttpPost]
        public ActionResult AddProduct(EditProduct product)
        {
            
            var newproduct = _manage.AddProduct(_manage.ToProduct(product));
            _manage.SaveProductImage(product, newproduct.ProductId);
            return RedirectToAction("Details", "Store", new { id = newproduct.ProductId });
        }
    }
}