using Domain.Modules.Base.Commands;

namespace Domain.Modules.CategoryOfProduct.Commands
{
    [Serializable]
    public class BaseCategoryOfProductCommand : BaseModuleCommand
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}