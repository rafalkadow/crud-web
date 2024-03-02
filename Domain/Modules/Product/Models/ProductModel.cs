using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.Product.Consts;
using Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Modules.CategoryOfProduct.Models;

namespace Domain.Modules.Product.Models
{
    /// <summary>
    /// Product
    /// </summary>
    [Serializable]
	[Index(nameof(Name), IsUnique = true)]
	[Table(ProductConsts.Table, Schema = BaseDatabaseConst.Base)]
	public class ProductModel : BaseModel, IEntity
	{
		#region Fields

		[Required]
		[StringLength(50)]
		public string? Name { get; set; }

		[StringLength(50)]
		public string? Code { get; set; }

        [Required]
        [ForeignKey("CategoryOfProductId")]
        public Guid CategoryOfProductId { get; set; }

        public virtual CategoryOfProductModel CategoryOfProduct { get; set; }

        public decimal Value { get; set; }
        public DateTime DateUtc { get; set; }
        public DateTime DateTimeUtc { get; set; }
        #endregion Fields
    }
}