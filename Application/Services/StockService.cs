using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Application.Validations;
using System.Linq.Expressions;
using Infrastructure.Shared.Models;
using Domain.Modules.ProductStock;
using Domain.Modules;
using Domain.Modules.Product;
using Application.Repositories.Interfaces;
using Domain.Modules.QueryStringParameters;
using Domain.Modules.Communication.Generics;
using Domain.Modules.Communication;

namespace Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        private readonly StockValidator _validator;
        private readonly StockParametersValidator _stockParametersValidator;

        public StockService(IStockRepository productStockRepository, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _stockRepository = productStockRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
            _validator = new StockValidator();
            _stockParametersValidator = new StockParametersValidator();
        }


        public async Task<ServiceResponse<PaginatedList<ProductStockReadDto>>> GetAllDtoPaginatedAsync(StockParametersDto parameters)
        {
            var validationResult = _stockParametersValidator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<PaginatedList<ProductStockReadDto>>(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid query string parameters. See '{BaseResponse.ErrorKey}' for more details");
            }

            Expression<Func<ProductStockApp, bool>> expression =
                s =>
                    (string.IsNullOrEmpty(parameters.ProductName) || s.Product.Name.ToLower().Contains(parameters.ProductName.ToLower())) &&
                    (!parameters.QuantityMin.HasValue || s.Quantity >= parameters.QuantityMin) &&
                    (!parameters.QuantityMax.HasValue || s.Quantity <= parameters.QuantityMax);


            var page = await _stockRepository.GetAllWherePaginatedAsync(parameters.PageNumber, parameters.PageSize, expression);

            var dto = _mapper.Map<PaginatedList<ProductStockApp>, PaginatedList<ProductStockReadDto>>(page);
            var result = new ServiceResponse<PaginatedList<ProductStockReadDto>>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductStockReadDto>> GetDtoByProductIdAsync(Guid productId)
        {
            var validationResult = new ValidationResult().ValidateId(productId, "Product Id");

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<ProductStockReadDto>(validationResult)
                    .SetDetail($"Invalid product data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var stock = await _stockRepository.GetByProductIdAsync(productId);
            if (stock == null)
            {
                return new FailedServiceResponse<ProductStockReadDto>()
                    .SetTitle("Stock not found.")
                    .SetDetail($"Stock for product [Id = {productId}]' not found.")
                    .SetStatus(StatusCodes.Status404NotFound);
            }
            var dto = _mapper.Map<ProductStockReadDto>(stock);

            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductStockApp>> GetByIdAsync(Guid id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return new FailedServiceResponse<ProductStockApp>()
                    .SetTitle("Stock not found")
                    .SetDetail($"Stock [Id = {id}] not found.")
                    .SetStatus(StatusCodes.Status404NotFound);
            }

            var result = new ServiceResponse<ProductStockApp>(stock);

            return result;
        }


        public async Task<ServiceResponse<ProductStockReadDto>> GetDtoByIdAsync(Guid id)
        {
            var validationResult = new ValidationResult().ValidateId(id, "Product stock Id");

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<ProductStockReadDto>(validationResult)
                    .SetDetail($"Invalid stock data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var stockResponse = await GetByIdAsync(id);
            if (!stockResponse.Success)
            {
                return new FailedServiceResponse<ProductStockReadDto>(stockResponse);
            }

            var stock = stockResponse.Data;
            var dto = _mapper.Map<ProductStockReadDto>(stock);

            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        // called by ProductService.Create only
        public ProductStockApp CreateProductStock(ProductApp product, int startingQuantity = AppConstants.Validations.Stock.QuantityMinValue)
        {
            var productStock = new ProductStockApp(product, startingQuantity);
            _stockRepository.Create(productStock);

            return productStock;
        }

        //test
        public async Task<ServiceResponse<ProductStockReadDto>> UpdateAsync(Guid id, ProductStockWriteDto stockUpdate)
        {
            var validationResult = _validator.Validate(stockUpdate)
                                             .ValidateId(id, "Product stock Id");

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<ProductStockReadDto>(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid stock data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var stockResponse = await GetByIdAsync(id);
            if (!stockResponse.Success)
            {
                return new FailedServiceResponse<ProductStockReadDto>(stockResponse);
            }

            var stock = stockResponse.Data;
            _mapper.Map(stockUpdate, stock);

            _stockRepository.Update(stock);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ProductStockReadDto>(stock);
            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductStockReadDto>> AddProductQuantityAsync(Guid id, int quantity)
        {
            var validationResult = new ValidationResult()
                                            .ValidateId(id, "Product stock Id")
                                            .GreaterThan(quantity, 0, "Quantity");

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<ProductStockReadDto>(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Error adding quantity to stock. See '{BaseResponse.ErrorKey}' for more details");
            }

            var stockResponse = await GetByIdAsync(id);
            if (!stockResponse.Success)
            {
                return new FailedServiceResponse<ProductStockReadDto>(stockResponse);
            }

            var stock = stockResponse.Data;

            if (stock.Quantity >= 0 && (stock.Quantity + quantity < 0)) //overflow test
            {
                return new FailedServiceResponse<ProductStockReadDto>()
                    .SetTitle("Operation error")
                    .SetDetail("Error adding to stock. Product stock quantity value will overflow. Contact the support")
                    .SetInstance(StockInstancePath(id));
            }

            stock.Quantity += quantity;
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ProductStockReadDto>(stock);
            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductStockReadDto>> RemoveProductQuantityAsync(Guid id, int quantity)
        {
            var validationResult = new ValidationResult()
                                            .ValidateId(id, "Product stock Id")
                                            .GreaterThan(quantity, 0, "Quantity");

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<ProductStockReadDto>(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid stock data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var stockResponse = await GetByIdAsync(id);
            if (!stockResponse.Success)
            {
                return new FailedServiceResponse<ProductStockReadDto>(stockResponse);
            }

            var stock = stockResponse.Data;

            if (stock.Quantity - quantity < AppConstants.Validations.Stock.QuantityMinValue)
            {
                return new FailedServiceResponse<ProductStockReadDto>()
                    .SetTitle($"Operation error")
                    .SetDetail($"Error removing product quantity from stock. Only [{stock.Quantity}] left on stock. Please insert a value less than or equal to {AppConstants.Validations.Stock.QuantityMinValue + stock.Quantity}")
                    .SetInstance(StockInstancePath(id));
            }

            stock.Quantity -= quantity;
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ProductStockReadDto>(stock);
            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        private string StockInstancePath(object id)
        {
            return _linkGenerator.GetPathByName("StockController.GetStockById", new { id });
        }
    }
}