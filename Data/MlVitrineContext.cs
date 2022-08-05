using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MlVitrine.Models;

namespace MlVitrine.Data
{
    public class MlVitrineContext : DbContext
    {
        public MlVitrineContext (DbContextOptions<MlVitrineContext> options)
            : base(options)
        {
        }

        public DbSet<MlVitrine.Models.Product> Product { get; set; } = default!;
        public DbSet<MlVitrine.Models.Brand> Brand { get; set; } = default!;
        public DbSet<MlVitrine.Models.ProductCategory> ProductCategory { get; set; } = default!;

        public DbSet<MlVitrine.Models.ProductCompatibility> ProductCompatibility { get; set; } = default!;
        public DbSet<MlVitrine.Models.ProductCondition> ProductCondition { get; set; } = default!;
        public DbSet<MlVitrine.Models.ProductImage> ProductImage { get; set; } = default!;
        public DbSet<MlVitrine.Models.ProductPriceHistory> ProductPrice { get; set; } = default!;
        public DbSet<MlVitrine.Models.ProductSpec> ProductSpec { get; set; } = default!;
        public DbSet<MlVitrine.Models.ProductStockHistory> ProductStock { get; set; } = default!;
        public DbSet<MlVitrine.Models.ProductWorkWith> ProductWorkWith { get; set; } = default!;
    }
}
