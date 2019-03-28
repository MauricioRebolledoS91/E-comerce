using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaleyApp.Models
{
    //Esta clase quedò convertida en partial, para poder utilizar el productMetadata.cs
    //y poder utilizar los dataanotations desde allì. con eso , esta clase modelo, queda màs limpia y no hay necesidad de agregarle
    //dataanotation a sus propiedades
    public partial class Product
    {
        public int ID { get; set; }

        
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProductImageMapping> ProductImageMappings { get; set; }
    }
}