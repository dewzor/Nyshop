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
    public class StoreService
    {
        private readonly StoreContext _db;

        public StoreService() : this(new StoreContext()) { } //Nollargumentskonstruktor
        
        public StoreService(StoreContext context) //konstruktor med egen context(db)
        {
            _db = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _db.Categories.OrderBy(c => c.Name).ToArrayAsync();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories.OrderBy(c => c.Name).ToArray();
        }

        public async Task<IEnumerable<Product>> GetProductsForAsync(string category)
        {
            return await _db.Products.Include("Category")
                .Where(p => p.Category.Name == category).ToArrayAsync();

        }

        public Category GetCategoryByID(int id)
        {
            return _db.Categories.Single(x => x.CategoryId == id);
        }

        public async Task<Product> GetProductByIDAsync(int id)
        {
            return await _db.Products.Include("Category")
                .Where(p => p.ProductId == id).SingleOrDefaultAsync();
        }

        public async Task<Product> GetProductByNameAsync(string name) //Needs some work.
        {
            return await _db.Products.Include("Category")
                .Where(p => p.Name == name).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _db.Products.OrderBy(c => c.Name).ToArrayAsync(); ;
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync() // Needs work, couldnt get it to work properly.
        {
            var products = await _db.Products.OrderBy(c => c.Name).ToArrayAsync();
            var prodlist = new HashSet<int>(products.Select(x => x.Category.CategoryId));
            //var categorylist = from c in _db.Categories 
                               //join p in _db.Products on c.CategoryId equals p.Category.CategoryId
                               
            var categorylist = from c in _db.Categories
                join p in _db.Products on c.CategoryId equals p.Category.CategoryId
                select c;
            var result = await categorylist.OrderBy(c => c.Name).GroupBy(c => c.Name).Select(c => c.FirstOrDefault())
                .ToArrayAsync();
                //_db.Categories.OrderBy(c => c.Name).Where(c => prodlist.Contains(c.CategoryId)).ToList();
            return categorylist;
        }

    }
}