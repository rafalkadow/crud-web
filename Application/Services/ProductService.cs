using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Application.Validations;
using System.Linq.Expressions;
using Infrastructure.Shared.Models;
using Domain.Modules.Product;
using Domain.Modules.QueryStringParameters;
using Application.Repositories.Interfaces;
using Domain.Modules;
using Domain.Modules.Communication.Generics;
using Domain.Modules.Communication;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductValidator _productValidator;
        private readonly ProductParametersValidator _productParametersValidator;

        public ProductService(IProductRepository productRepository, IStockService stockService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productValidator = new ProductValidator();
            _productParametersValidator = new ProductParametersValidator();
        }



        public async Task<ServiceResponse<PaginatedList<ProductReadDto>>> GetAllDtoPaginatedAsync(ProductParametersDto parameters)
        {
            var validationResult = _productParametersValidator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<PaginatedList<ProductReadDto>>(validationResult)
                            .SetDetail($"Invalid query string parameters. See '{BaseResponse.ErrorKey}' for more details");
            }

            Expression<Func<ProductApp, bool>> expression =
                p =>
                    (!parameters.MinPrice.HasValue || p.Price >= parameters.MinPrice) &&
                    (!parameters.MaxPrice.HasValue || p.Price <= parameters.MaxPrice) &&
                    (string.IsNullOrEmpty(parameters.Name) || p.Name.ToLower().Contains(parameters.Name.ToLower())) &&
                    (string.IsNullOrEmpty(parameters.Description) || p.Description.ToLower().Contains(parameters.Description.ToLower())) &&
                    (!parameters.OnStock.HasValue || ((p.ProductStock.Quantity > 0) == parameters.OnStock.Value));

            var page = await _productRepository.GetAllWherePaginatedAsync(parameters.PageNumber, parameters.PageSize, expression);
            var dto = _mapper.Map<PaginatedList<ProductApp>, PaginatedList<ProductReadDto>>(page);

            var result = new ServiceResponse<PaginatedList<ProductReadDto>>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductReadWithStockDto>> CreateAsync(ProductWriteDto productDto, int quantity)
        {
            var validationResult = new ValidationResult()
                        .GreaterThanOrEqualTo(quantity, AppConstants.Validations.Stock.QuantityMinValue, "Quantity")
                        .AddFailuresFrom(_productValidator.Validate(productDto));

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<ProductReadWithStockDto>(validationResult)
                            .SetDetail($"Invalid product data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var product = _mapper.Map<ProductApp>(productDto);
            _productRepository.Add(product);

            _stockService.CreateProductStock(product, quantity);

            await _unitOfWork.CompleteAsync();

            var productWithStockDto = _mapper.Map<ProductReadWithStockDto>(product);
            var result = new ServiceResponse<ProductReadWithStockDto>(productWithStockDto);

            return result;
        }


        public async Task<ServiceResponse<ProductReadDto>> GetDtoByIdAsync(Guid id)
        {
            var response = await GetByIdAsync(id);
            if (!response.Success)
            {
                return new FailedServiceResponse<ProductReadDto>(response.Error);
            }

            var dto = _mapper.Map<ProductReadDto>(response.Data);
            var result = new ServiceResponse<ProductReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductApp>> GetByIdAsync(Guid id)
        {
            var validationResponse = new ValidationResult().ValidateId(id, "Product Id");
            if (!validationResponse.IsValid)
            {
                return new FailedServiceResponse<ProductApp>(validationResponse)
                            .SetDetail($"Invalid product data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new FailedServiceResponse<ProductApp>()
                            .SetTitle("Product not found")
                            .SetDetail($"Product [Id = {id}] not found.")
                            .SetStatus(StatusCodes.Status404NotFound);
            }

            var result = new ServiceResponse<ProductApp>(product);

            return result;
        }


        public async Task<ServiceResponse<ProductReadDto>> UpdateAsync(Guid id, ProductWriteDto productDto)
        {
            var validationResult = new ValidationResult()
                        .ValidateId(id, "Product Id")
                        .AddFailuresFrom(_productValidator.Validate(productDto));

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<ProductReadDto>(validationResult)
                            .SetDetail($"Invalid product data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var productResponse = await GetByIdAsync(id);
            if (!productResponse.Success)
            {
                return new FailedServiceResponse<ProductReadDto>(productResponse.Error);
            }

            var product = productResponse.Data;

            _mapper.Map(productDto, product);
            _productRepository.Update(product);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ProductReadDto>(product);
            var result = new ServiceResponse<ProductReadDto>(dto);

            return result;
        }


        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var validationResult = new ValidationResult().ValidateId(id, "Product Id");
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse(validationResult)
                            .SetDetail($"Invalid product data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var response = await GetByIdAsync(id);
            if (!response.Success)
            {
                return new FailedServiceResponse(response.Error);
            }

            var product = response.Data;

            _productRepository.Delete(product);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse();
        }
    }
}