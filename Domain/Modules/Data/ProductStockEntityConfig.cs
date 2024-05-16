using Domain.Modules.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Modules.Data
{
    public class ProductStockEntityConfig : IEntityTypeConfiguration<ProductStockApp>
    {
        public void Configure(EntityTypeBuilder<ProductStockApp> builder)
        {
            builder.HasData(SeedData.ProductStocks);
            //    new List<ProductStock>
            //{
            //    new ProductStock
            //    {
            //        Id = 1,
            //        ProductId = 1,
            //        Quantity = 200
            //    },

            //    new ProductStock
            //    {
            //        Id = 2,
            //        ProductId = 2,
            //        Quantity = 100
            //    },

            //    new ProductStock
            //    {
            //        Id = 3,
            //        ProductId = 3,
            //        Quantity = 50
            //    },

            //    new ProductStock
            //    {
            //        Id = 4,
            //        ProductId = 4,
            //        Quantity = 150
            //    },

            //    new ProductStock
            //    {
            //        Id = 5,
            //        ProductId = 5,
            //        Quantity = 200
            //    }
            //});
        }
    }
}