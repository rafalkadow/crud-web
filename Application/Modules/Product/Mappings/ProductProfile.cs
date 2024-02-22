using AutoMapper;
using Domain.Modules.Product.Commands;
using Domain.Modules.Product.Models;
using Domain.Modules.Product.Queries;
using Domain.Modules.Product.ViewModels;

namespace Application.Modules.Product.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, ProductModel>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductModel>().ReverseMap();

            CreateMap<ProductViewModel, GetProductResultById>().ReverseMap();
            CreateMap<CreateProductCommand, GetProductResultById>().ReverseMap();

            CreateMap<GetProductResultById, UpdateProductCommand>().ReverseMap();
            CreateMap<GetProductResultAll, UpdateProductCommand>().ReverseMap();

            CreateMap<ProductModel, GetProductResultAll>().ReverseMap();
            CreateMap<ProductModel, GetProductResultById>().ReverseMap();
        }
    }
}