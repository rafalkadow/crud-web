using Domain.Modules.Base.Enums;
using Domain.Modules.Base.Menu;
using Domain.Modules.Base.Models;
using Domain.Modules.Base.Values;
using Domain.Modules.Base.ViewModels;
using Domain.Modules.CategoryOfProduct.Consts;
using Domain.Modules.CategoryOfProduct.Menu;
using Domain.Modules.Interfaces;

namespace Domain.Modules.CategoryOfProduct.Values
{
    [Serializable]
    public class CategoryOfProductValue : ValueBase, IValue
    {
        public CategoryOfProductValue(IDefinitionModel definitionModel)
           : base(definitionModel)
        {
        }

        public override string ModuleUrl()
        {
            return CategoryOfProductConsts.Url;
        }

        public override string ControllerName()
        {
            return CategoryOfProductConsts.ControllerName;
        }

        public override string ModuleTitle()
        {
            return CategoryOfProductConsts.Title;
        }

        public override MenuElementEnum SubMenuElementName()
        {
            return MenuElementEnum.CategoryOfProduct;
        }

        public override MenuElement DataMenu(ValueViewModel valueModel)
        {
            var menu = CategoryOfProductMenu.MenuToObject(valueModel);
            return menu;
        }
        public override string SuccessMessageTitle()
        {
            return "A new category of product has been added";
        }
        public override string SuccessMessageAnotherTitle()
        {
            return "The category of product has been updated";
        }
        public override string ErrorMessageTitle()
        {
            return "The category of product could not be added";
        }
        public override string ErrorMessageAnotherTitle()
        {
            return "The category of product could not be updated";
        }
    }
}