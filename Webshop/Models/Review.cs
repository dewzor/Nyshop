using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class Review
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string CustomerId { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }
    }
}