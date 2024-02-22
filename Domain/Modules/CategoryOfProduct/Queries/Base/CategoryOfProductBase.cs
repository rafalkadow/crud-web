using Domain.Modules.Base.Queries;

namespace Domain.Modules.CategoryOfProduct.Queries
{
	[Serializable]
	public class GetCategoryOfProductBase : GetBaseResultFilter
	{
		public string? Name { get; set; }
		public string? Code { get; set; }
    }
}