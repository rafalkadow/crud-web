namespace Domain.Modules.Product
{
    /// <summary>
    /// Data about a registered product 
    /// </summary>
    public class ProductReadDto
    {
        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000001</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        /// <example>Banana</example>
        public string Name { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        /// <example>Banana caturra 1kg</example>
        public string Description { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        /// <example>5.99</example>
        public double Price { get; set; }

        /// <summary>
        /// Product stock id
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000001</example>
        public Guid ProductStockId { get; set; }
    }
}