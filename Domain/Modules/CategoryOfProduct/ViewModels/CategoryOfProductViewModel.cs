using Domain.Modules.Base.Models;
using Domain.Modules.Base.Validations;
using Domain.Modules.Base.ViewModels;
using Domain.Modules.CategoryOfProduct.Values;

namespace Domain.Modules.CategoryOfProduct.ViewModels
{
    public class CategoryOfProductViewModel : ValueViewModel
    {
        #region Fields
        public string? Name { get; set; }

        public string? Code { get; set; }

        #endregion Fields

        #region Constructors

        public CategoryOfProductViewModel(IDefinitionModel model)
             : base(ChooseOperationType(model))
        {
            ViewName = ControllerNameWithOperation();
        }

        #endregion Constructors

        #region ChooseOperationType

        public static IDefinitionModel ChooseOperationType(IDefinitionModel model)
        {
            var value = new CategoryOfProductValue(model);
            var validation = new ValidationBase(model);
            model.ApplicationValue = value;
            model.ApplicationValidation = validation;
            return model;
        }

        #endregion ChooseOperationType
    }
}