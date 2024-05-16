namespace Domain.Modules.Product
{
    public class ProductApp
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ProductStockApp ProductStock { get; set; }

        public Guid ProductStockId => ProductStock.Id;


        public ProductApp()
        {
        }

        public ProductApp(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}