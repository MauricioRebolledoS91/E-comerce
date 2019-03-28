using MaleyApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MaleyApp.DAL
{
    public class StoreContext: DbContext
    {
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<ProductImage> ProductImages { get; set; }
            public DbSet<ProductImageMapping> ProductImageMappings { get; set; }
    }
}