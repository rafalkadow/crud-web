using Application.Helpers;
using AutoMapper;
using Domain.Modules;
using Domain.Modules.Auth;
using Domain.Modules.Identity;
using Domain.Modules.Product;
using Domain.Modules.ProductStock;
using Domain.Modules.Role;
using Domain.Modules.TestUser;
using Domain.Modules.User;

namespace Application.Modules.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Product
            CreateMap<ProductWriteDto, ProductApp>().ReverseMap();
            CreateMap<ProductReadDto, ProductApp>()
                .ReverseMap();
            CreateMap<ProductApp, ProductReadWithStockDto>().ReverseMap();

            //PaginatedList
            CreateMap<PaginatedList<ProductApp>, PaginatedList<ProductReadDto>>();
            CreateMap<PaginatedList<RoleApp>, PaginatedList<RoleReadDto>>();
            CreateMap<PaginatedList<UserApp>, PaginatedList<UserReadDto>>();
            CreateMap<PaginatedList<ProductStockApp>, PaginatedList<ProductStockReadDto>>();

            //ProductStock
            CreateMap<ProductStockReadDto, ProductStockApp>().ReverseMap();
            CreateMap<ProductStockWriteDto, ProductStockApp>().ReverseMap();

            //User
            CreateMap<UserApp, UserLoginDto>().ReverseMap();
            CreateMap<UserApp, UserUpdateDto>().ReverseMap();
            CreateMap<UserApp, UserAuthDto>().ReverseMap();
            CreateMap<UserApp, UserRegisterDto>().ReverseMap();
            CreateMap<UserRegisterDto, UserLoginDto>().ReverseMap();
            CreateMap<UserApp, UserReadDto>()
                .ForMember(dto => dto.FullName, o => o.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dto => dto.Age, o => o.MapFrom(src => AgeCalculator.Calculate(src.DateOfBirth)));
            CreateMap<UserApp, UserDetailedReadDto>()
                .ForMember(dto => dto.Age, o => o.MapFrom(src => AgeCalculator.Calculate(src.DateOfBirth)));

            //Role
            CreateMap<RoleApp, RoleReadDto>().ReverseMap();
            CreateMap<RoleApp, RoleWriteDto>().ReverseMap();

            //RandomedUser
            CreateMap<RandomedUser, UserRegisterDto>()
                .ForMember(dest => dest.DateOfBirth, o => o.MapFrom(src => src.Dob.Date))
                .ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.Name.First))
                .ForMember(dest => dest.LastName, o => o.MapFrom(src => src.Name.Last));

        }
    }
}