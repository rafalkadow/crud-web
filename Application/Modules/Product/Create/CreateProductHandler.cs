using AutoMapper;
using MediatR;
using Shared.Models;
using Domain.Modules.Product.Commands;
using Domain.Interfaces;
using Shared.Extensions.GeneralExtensions;
using Domain.Modules.Product.Models;
using Shared.Enums;
using NLog;
using Microsoft.Extensions.Logging;
using Application.Modules.Base.Commands;
using Domain.Modules.Communication.Generics;

namespace Application.Modules.Product.Create
{
    [Serializable]
    public class CreateProductHandler : BaseCommandHandler, IRequestHandler<CreateProductCommand, ServiceResponse<OperationResult>>
    {
        public CreateProductHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<ServiceResponse<OperationResult>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(filter='{command.RenderProperties()}', cancellationToken='{cancellationToken}')");
            try
            {

                var model = Mapper.Map<ProductModel>(command);
                model.OrderId = await GetOrderIdForTable(model);
                await DbContext.CreateAsync(model, UserAccessor);

                return new ServiceResponse<OperationResult>(new OperationResult(model, OperationEnum.Create));
            }
            catch (Exception ex)
            {
                logger.Error($"Handle(ex='{ex.ToString()}')");
                throw;
            }
        }


    }
}