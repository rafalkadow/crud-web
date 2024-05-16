using Domain.Modules.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Modules.Data
{
    public class ProductEntityConfig : IEntityTypeConfiguration<ProductApp>
    {
        public void Configure(EntityTypeBuilder<ProductApp> builder)
        {
            builder.HasData(SeedData.Products);
        }
    }
}