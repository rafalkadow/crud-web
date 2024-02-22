using Domain.Modules.Base.Menu;
using Domain.Modules.Base.ViewModels;
using Domain.Modules.Product.Values;

namespace Domain.Modules.Product.Menu
{
    [Serializable]
    public class ProductMenu : BaseMenu
    {
        public static MenuElement MenuToObject(ValueViewModel valueModel)
        {
            var value = new ProductValue(valueModel.Definition);

            var menu = GetMenu(value, valueModel);
            return menu;
        }
    }
}