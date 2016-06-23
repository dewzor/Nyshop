using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class Review
    {
        public int ProductId { get; set; }
        public string CustomerId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }
    }
}