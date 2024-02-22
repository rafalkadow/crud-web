using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.CategoryOfProduct.Consts;
using Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Modules.Product.Models;

namespace Domain.Modules.CategoryOfProduct.Models
{
    /// <summary>
    /// Category Of Product
    /// </summary>
    [Serializable]
	[Index(nameof(Name), IsUnique = true)]
	[Table(CategoryOfProductConsts.Table, Schema = BaseDatabaseConst.Base)]
	public class CategoryOfProductModel : BaseModel, IEntity
	{
		#region Fields

		[Required]
		[StringLength(100)]
		public string? Name { get; set; }

		[StringLength(50)]
		public string? Code { get; set; }

        public ICollection<ProductModel> Product { get; set; }

        #endregion Fields
    }
}