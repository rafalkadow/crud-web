using Domain.Modules.Base.Menu;
using Domain.Modules.Base.ViewModels;
using Domain.Modules.CategoryOfProduct.Values;

namespace Domain.Modules.CategoryOfProduct.Menu
{
    [Serializable]
    public class CategoryOfProductMenu : BaseMenu
    {
        public static MenuElement MenuToObject(ValueViewModel valueModel)
        {
            var value = new CategoryOfProductValue(valueModel.Definition);

            var menu = GetMenu(value, valueModel);
            return menu;
        }
    }
}