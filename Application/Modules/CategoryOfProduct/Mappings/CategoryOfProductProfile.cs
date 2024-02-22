using AutoMapper;
using Domain.Modules.CategoryOfProduct.Commands;
using Domain.Modules.CategoryOfProduct.Models;
using Domain.Modules.CategoryOfProduct.Queries;
using Domain.Modules.CategoryOfProduct.ViewModels;

namespace Application.Modules.CategoryOfProduct.Mappings
{
    public class CategoryOfProductProfile : Profile
    {
        public CategoryOfProductProfile()
        {
            CreateMap<CreateCategoryOfProductCommand, CategoryOfProductModel>().ReverseMap();
            CreateMap<UpdateCategoryOfProductCommand, CategoryOfProductModel>().ReverseMap();

            CreateMap<CategoryOfProductViewModel, GetCategoryOfProductResultById>().ReverseMap();
            CreateMap<CreateCategoryOfProductCommand, GetCategoryOfProductResultById>().ReverseMap();

            CreateMap<GetCategoryOfProductResultById, UpdateCategoryOfProductCommand>().ReverseMap();
            CreateMap<GetCategoryOfProductResultAll, UpdateCategoryOfProductCommand>().ReverseMap();

            CreateMap<CategoryOfProductModel, GetCategoryOfProductResultAll>().ReverseMap();
            CreateMap<CategoryOfProductModel, GetCategoryOfProductResultById>().ReverseMap();
        }
    }
}