namespace Domain.Modules.ProductStock
{
    /// <summary>
    /// Product stock information
    /// </summary>
    public class ProductStockReadDto
    {
        /// <summary>
        /// Product stock Id
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000001</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000001</example>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        /// <example>Banana</example>
        public string ProductName { get; set; }

        /// <summary>
        /// Product quantity on stock
        /// </summary>
        /// <example>9000</example>
        public int Quantity { get; set; }
    }
}