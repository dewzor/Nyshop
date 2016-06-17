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
            var result = check.IsImage(file.ProductImage); //Checks thats the uploaded file is an image.
            string extension = Path.GetExtension(file.ProductImage.FileName); //Stores files extension for saving correct filetype.
            if (result && file.ProductImage.ContentLength < 3000000) //If its an image AND its under 3mb, upload it.
            {
                string path = Server.MapPath("~/Images/Products/" + file.ProductId + "/" + file.Name + extension);
                if(!Directory.Exists("~/Images/Products/" + file.ProductId + "/"))
                    Directory.CreateDirectory("~/Images/Products/" + file.ProductId);
                
                file.ProductImage.SaveAs(path);
                file.ImageUrl = "~/Images/Products/" + file.ProductId + "/" + file.Name + extension;
                _manage.UpdateProductImage(file);
            }
            
            return RedirectToAction("EditProduct", "Admin", new { id = file.ProductId});
        }
        [HttpPost]
        public ActionResult AddProduct(EditProduct product)
        {
            var newproductid = _manage.GetMaxProductId() + 1;
            //First uploads image.
            FormLogic check = new FormLogic();
            var result = check.IsImage(product.ProductImage); //Checks thats the uploaded file is an image.
            string extension = Path.GetExtension(product.ProductImage.FileName); //Stores files extension for saving correct filetype.
            if (result && product.ProductImage.ContentLength < 3000000) //If its an image AND its under 3mb, upload it.
            {
                string path = Server.MapPath("~/Images/Products/" + newproductid + "/" + product.Name + extension);
                if (!Directory.Exists("~\\Images\\Products\\" + newproductid+"\\"))
                    Directory.CreateDirectory("~/Images/Products/" + newproductid);

                product.ProductImage.SaveAs(path);
                product.ImageUrl = "~/Images/Products/" + newproductid + "/" + product.Name + extension;
                _manage.UpdateProductImage(product);
            }
            _manage.AddProduct(_manage.ToProduct(product));
            
            return RedirectToAction("Details", "Store", new { id = newproductid });
        }
    }
}