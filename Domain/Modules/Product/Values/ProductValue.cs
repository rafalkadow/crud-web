using Domain.Modules.Base.Enums;
using Domain.Modules.Base.Menu;
using Domain.Modules.Base.Models;
using Domain.Modules.Base.Values;
using Domain.Modules.Base.ViewModels;
using Domain.Modules.Product.Consts;
using Domain.Modules.Product.Menu;
using Domain.Modules.Interfaces;

namespace Domain.Modules.Product.Values
{
    [Serializable]
    public class ProductValue : ValueBase, IValue
    {
        public ProductValue(IDefinitionModel definitionModel)
           : base(definitionModel)
        {
        }

        public override string ModuleUrl()
        {
            return ProductConsts.Url;
        }

        public override string ControllerName()
        {
            return ProductConsts.ControllerName;
        }

        public override string ModuleTitle()
        {
            return ProductConsts.Title;
        }

        public override MenuElementEnum SubMenuElementName()
        {
            return MenuElementEnum.Product;
        }

        public override MenuElement DataMenu(ValueViewModel valueModel)
        {
            var menu = ProductMenu.MenuToObject(valueModel);
            return menu;
        }
        public override string SuccessMessageTitle()
        {
            return "A new product has been added";
        }
        public override string SuccessMessageAnotherTitle()
        {
            return "The product has been updated";
        }
        public override string ErrorMessageTitle()
        {
            return "The product could not be added";
        }
        public override string ErrorMessageAnotherTitle()
        {
            return "The product could not be updated";
        }
    }
}