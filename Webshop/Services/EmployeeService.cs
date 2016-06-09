using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Services
{
    public class EmployeeService
    {
        private StoreContext _db;

        public EmployeeService() : this(new StoreContext()) { } //Nollargumentskonstruktor

        public EmployeeService(StoreContext context) //konstruktor med egen context(db)
        {
            _db = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _db.Categories.OrderBy(c => c.Name).ToArrayAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsForAsync(string category)
        {
            return await _db.Products.Include("Category")
                .Where(p => p.Category.Name == category).ToArrayAsync();

        }




    }
}