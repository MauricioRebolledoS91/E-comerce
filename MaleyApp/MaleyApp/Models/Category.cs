using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaleyApp.Models
{
    public partial class Category
    {
        public int ID { get; set; }

       
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}