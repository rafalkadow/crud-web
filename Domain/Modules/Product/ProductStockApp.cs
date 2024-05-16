namespace Domain.Modules.Product
{
    public class ProductStockApp
    {
        public Guid Id { get; set; }
        public ProductApp Product { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public ProductStockApp()
        {
        }

        public ProductStockApp(ProductApp product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}