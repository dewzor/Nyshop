using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Braintree;
using Webshop.Logic;
using Webshop.Models;
using Webshop.Services;

namespace Webshop.Data
{
    public class StoreManager
    {
        private StoreService _service = new StoreService();

        public Product AddProduct(Product product) //Returns ID of the new product.
        {
            using (var _db = new StoreContext())
            {
                _db.Products.AddOrUpdate(product);

                _db.SaveChanges();
                UpdateProductCategory(product.ProductId, GetCategoryIdByName(product.CategoryName));
                _db.SaveChanges();

            }
            return product; //When the product is saved to DB it gets an ID.
        }
        public void UpdateProduct(Product product)
        {
            using (var _db = new StoreContext())
            {
                //Category current = _service.GetCategoryByIDAsync(product.Category.CategoryId);
                var dbproduct = _db.Products.SingleOrDefault(x => x.ProductId == product.ProductId);
                if (dbproduct != null)
                {
                    if (product.Name != null)
                        dbproduct.Name = product.Name;

                    UpdateProductCategory(product.ProductId, product.Category.CategoryId);
                    if (dbproduct.Price != product.Price)
                        dbproduct.Price = product.Price;

                    if (product.Description != null && dbproduct.Description != product.Description)
                        dbproduct.Description = product.Description;
                    if (product.Published != dbproduct.Published)
                        dbproduct.Published = product.Published;

                    _db.Entry(dbproduct).State = EntityState.Modified;
                        // Tells entity framework that value is changed(?)
                    _db.SaveChanges();
                }
            }

        }

        public void UpdateProductImage(int product, string imageurl) // Just testing, dont know if it will work.
        {
            using (var _db = new StoreContext())
            {
                var dbproduct = _db.Products.Single(x => x.ProductId == product);
                dbproduct.ImageUrl = imageurl;
                _db.Entry(dbproduct).State = EntityState.Modified; // Tells entity framework that value is changed(?)
                _db.SaveChanges();
            }

        }

        public Product ToProduct(EditProduct product)
        {
            Product result = new Product();

            result.Name = product.Name;
            result.Description = product.Description;
            result.Published = product.Published;
            result.CategoryName = product.CategoryName;
            result.Price = product.Price;
            result.ImageUrl = product.ImageUrl;
            result.ProductId = product.ProductId;
            result.Category = product.Category;

            return result;
        }

        public EditProduct ToEditProduct(Product product)
        {
            EditProduct result = new EditProduct();

            result.Name = product.Name;
            result.Description = product.Description;
            result.Published = product.Published;
            result.CategoryName = product.CategoryName;
            result.Price = product.Price;
            result.ImageUrl = product.ImageUrl;
            result.ProductId = product.ProductId;
            result.Category = product.Category;

            return result;
        }

        public void UpdateProductCategory(int productid, int categoryid)
        {
            using (var _db = new StoreContext())
            {
                _db.Database.ExecuteSqlCommand("UPDATE PRODUCTS SET Category_CategoryId ='" +
                                               categoryid + "' WHERE ProductId=" + productid);
            }
        }

        public int GetCategoryIdByName(string categoryname)
        {
            using (var _db = new StoreContext())
            {
                return
                    _db.Database.SqlQuery<int>("SELECT CategoryId from Categories where Name='" + categoryname + "'")
                        .First();
            }

        }

        public int GetMaxProductId()
        {
            using (var _db = new StoreContext())
            {
                return _db.Database.SqlQuery<int>("SELECT MAX(ProductId) from products").First();
            }
        }

        public void SaveProductImage(EditProduct product, int id)
        {
            FormLogic check = new FormLogic();
            var result = check.IsImage(product.ProductImage); //Checks thats the uploaded file is an image.
            string extension = Path.GetExtension(product.ProductImage.FileName); //Stores files extension for saving correct filetype.
            if (result && product.ProductImage.ContentLength < 3000000) //If its an image AND its under 3mb, upload it.
            {
                string folderPath = HttpContext.Current.Server.MapPath("~/Images/Products/" + id + "/");
                string path = Path.Combine(folderPath + product.Name + extension);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                product.ProductImage.SaveAs(path);
                product.ImageUrl = "~/Images/Products/" + id + "/" + product.Name + extension;

                UpdateProductImage(id, product.ImageUrl);
            }
        }

        public void AddCategory(string categoryname)
        {
            using (var _db = new StoreContext())
            {
                Category category = new Category();
                category.Name = categoryname;
                _db.Categories.AddOrUpdate(category);
                //_db.Entry(category).State = EntityState.Modified; // Doesnt add/commit to db when this is used, have to read up on it.
                _db.SaveChanges();
            }
        }

        
    }
}