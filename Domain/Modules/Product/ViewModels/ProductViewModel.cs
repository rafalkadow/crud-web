﻿using Domain.Modules.Base.Models;
using Domain.Modules.Base.Validations;
using Domain.Modules.Base.ViewModels;
using Domain.Modules.Product.Values;
using System.ComponentModel.DataAnnotations;
using Domain.Modules.CategoryOfProduct.Models;

namespace Domain.Modules.Product.ViewModels
{
    public class ProductViewModel : ValueViewModel
    {
        #region Fields
        public string? Name { get; set; }

        public string? Code { get; set; }

        public Guid CategoryOfProductId { get; set; }

        public virtual CategoryOfProductModel CategoryOfProduct { get; set; }

        public string? ContractorName { get; set; }
        #endregion Fields

        #region Constructors

        public ProductViewModel(IDefinitionModel model)
             : base(ChooseOperationType(model))
        {
            ViewName = ControllerNameWithOperation();
        }

        #endregion Constructors

        #region ChooseOperationType

        public static IDefinitionModel ChooseOperationType(IDefinitionModel model)
        {
            var value = new ProductValue(model);
            var validation = new ValidationBase(model);
            model.ApplicationValue = value;
            model.ApplicationValidation = validation;
            return model;
        }

        #endregion ChooseOperationType
    }
}