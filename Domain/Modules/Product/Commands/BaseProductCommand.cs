using Domain.Modules.Base.Commands;
using Domain.Modules.CategoryOfProduct.Models;

namespace Domain.Modules.Product.Commands
{
    [Serializable]
    public class BaseProductCommand : BaseModuleCommand
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public Guid CategoryOfProductId { get; set; }

        public virtual CategoryOfProductModel CategoryOfProduct { get; set; }

    }
}