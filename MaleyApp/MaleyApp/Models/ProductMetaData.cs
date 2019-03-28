using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaleyApp.Models
{
        //Estas clases son usadas para poner aquì los dataanotations
        //y poder dejar limpias las clases modelo 
        [MetadataType(typeof(ProductMetaData))]
        public partial class Product
        {
        }
        public class ProductMetaData
        {
        [Required(ErrorMessage = "El nombre de Producto no puede estar en blanco")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please ingre una categoría entre 3 and 50 carácteres de longitud")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Por favor ingrese un nombre de producto que empiece con letra mayúscula")]  //el campo solo debe tener letras y espacios y comenzar con una letra mayúsucla
        [Display(Name = "Nombre Producto")]
        public string Name;

        [Required(ErrorMessage = "La descripción del producto no puede estar en blanco")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Por favor ingresa una descripción de produco entre 10 y 200 carácteres de longitud")]
        [RegularExpression(@"^[,;a-zA-Z0-9'-'\s]*$", ErrorMessage = "Por favor ingresa una descripción de producto únicamente en letras y números")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "El precio no puede estar en blanco")]
        [Range(0.10, 10000, ErrorMessage = "Por favor ingresa un precio entre 0.10 y 10000.00")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "El precio debe ser un número de hasta dos decimales.")]
        public decimal Price { get; set; }
    }
    
}