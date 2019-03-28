using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaleyApp.Models
{
    
        [MetadataType(typeof(CategoryMetaData))]
        public partial class Category
        {
        }

        public class CategoryMetaData
        {

        [Required(ErrorMessage = "El nombre de categoría no puede estar en blanco")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please ingre una categoría entre 3 and 50 carácteres de longitud")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Por favor ingrese un nombre de categoría que empiece con letra mayúscula")]  //el campo solo debe tener letras y espacios y comenzar con una letra mayúsucla
        [Display(Name = "Nombre Categoría")]
            public string Name;
        }
    
}