using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaleyApp.Models
{
    public class ProductImage
    {
            public int ID { get; set; }

            [Display(Name = "Archivo")]
            [StringLength(100)]
            [Index(IsUnique = true)]//Este dataanotation lo agregamos para que solo se pueda agregar una imagen con el mismo nombre, solo una vez
            public string FileName { get; set; }

            public virtual ICollection<ProductImageMapping> ProductImageMappings { get; set; }


    }
}