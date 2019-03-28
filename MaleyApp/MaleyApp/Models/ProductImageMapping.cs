using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaleyApp.Models
{
    //Para modelar una combinación de muchos a muchos con carga útil, necesita una nueva entidad
    public class ProductImageMapping
    {
        
        public int ID { get; set; }
        public int ImageNumber { get; set; }
        public int ProductID { get; set; }
        public int ProductImageID { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductImage ProductImage { get; set; }
    }
}