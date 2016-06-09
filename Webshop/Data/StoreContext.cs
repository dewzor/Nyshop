using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Webshop.Models;

namespace Webshop.Data
{
    
    public class StoreContext : IdentityDbContext<ApplicationUser> //DbContext
    {
        
        public StoreContext() : base("Webshop") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public static StoreContext Create()
        {
            return new StoreContext();
        }
    }


}