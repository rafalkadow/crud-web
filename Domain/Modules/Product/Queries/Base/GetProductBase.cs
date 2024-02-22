using Domain.Modules.Base.Queries;
using Domain.Modules.CategoryOfProduct.Models;

namespace Domain.Modules.Product.Queries
{
	[Serializable]
	public class GetProductBase : GetBaseResultFilter
	{
		public string? Name { get; set; }
		public string? Code { get; set; }
        public Guid CategoryOfProductId { get; set; }

        public virtual CategoryOfProductModel CategoryOfProduct { get; set; }

    }
}