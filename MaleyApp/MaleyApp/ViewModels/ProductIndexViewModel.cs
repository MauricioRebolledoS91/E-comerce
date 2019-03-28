using MaleyApp.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaleyApp.ViewModels
{
    public class ProductIndexViewModel
    {
        public IPagedList<Product> Products { get; set; }
        public string Search { get; set; }
        public IEnumerable<CategoryWithCount> CatsWithCount { get; set; }
        public string Category { get; set; }
        public string SortBy { get; set; }//La propiedad SortBy se usará como el nombre del elemento seleccionado en la vista 
        public Dictionary<string, string> Sorts { get; set; }//la propiedad Sorts se utilizará para mantener los datos para rellenar el elemento seleccionado
        public IEnumerable<SelectListItem> CatFilterItems
        {
            get
            {
                var allCats = CatsWithCount.Select(cc => new SelectListItem
                {
                    Value = cc.CategoryName,
                    Text = cc.CatNameWithCount
                });
                return allCats;
            }
        }
    }

    //es una clase simple que se usa para mantener un nombre de categoría y el número de productos dentro de ese
    //categoría.
    public class CategoryWithCount
    {
        //contiene el número de productos coincidentes en una categoría
        public int ProductCount { get; set; }

        //simplemente tiene el nombre de la categoría.
        public string CategoryName { get; set; }

        //devuelve ambas
        //propiedades combinadas en una cadena.
        public string CatNameWithCount
        {
            get
            {
                return CategoryName + " (" + ProductCount.ToString() + ")";
            }
        }

    }
} 
