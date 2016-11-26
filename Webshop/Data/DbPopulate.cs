using Webshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Webshop.Data
{
    public class DbPopulate
    {
        public async static Task AddDataAsync()
        {
            using (var db = new StoreContext())
            {

                if (db.Categories.Any())
                    return;

                var computers = new Category { Name = "Computers" };
                var printers = new Category { Name = "Printers" };
                var books = new Category { Name = "Books" };

                db.Categories.AddRange(new Category[] { computers, printers, books });

                db.Products.Add(new Models.Product
                {
                    Category = computers,
                    Name = "Really Fast Computer",
                    ImageUrl = "~/Images/leaves.jpg"
                });

                db.Products.Add(new Models.Product
                {
                    Category = computers,
                    Name = "Mainstream Computer",
                    ImageUrl = "~/Images/leaves.jpg"
                });

                db.Products.Add(new Models.Product
                {
                    Category = printers,
                    Name = "Fast Color Laser Printer",
                    ImageUrl = "~/Images/leaves.jpg"
                });

                db.Products.Add(new Models.Product
                {
                    Category = printers,
                    Name = "Normal Ink Jet Printer",
                    ImageUrl = "~/Images/leaves.jpg"
                });

                db.Products.Add(new Models.Product
                {
                    Category = printers,
                    Name = "Dot Matrix Printer",
                    ImageUrl = "~/Images/leaves.jpg"
                });

                db.Products.Add(new Models.Product
                {
                    Category = books,
                    Name = "Really Interesting Book",
                    ImageUrl = "~/Images/leaves.jpg"
                });

                db.Products.Add(new Models.Product
                {
                    Category = books,
                    Name = "Boring Book You Have to Read",
                    ImageUrl = "~/Images/leaves.jpg"
                });

                db.Products.Add(new Models.Product
                {
                    Category = books,
                    Name = "One of My Books",
                    ImageUrl = "~/Images/leaves.jpg"
                });

                await db.SaveChangesAsync();
            }
        }
    }
}