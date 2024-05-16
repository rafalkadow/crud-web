using Domain.Modules.Base.Commands;

namespace Domain.Modules.CategoryOfProduct.Commands
{
    [Serializable]
    public class BaseCategoryOfProductCommand : BaseModuleCommand
    {
        /// <summary>
        /// Name
        /// </summary>
        /// <example>Name1</example>
        public string? Name { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        /// <example>Code1</example>
        public string? Code { get; set; }
    }
}