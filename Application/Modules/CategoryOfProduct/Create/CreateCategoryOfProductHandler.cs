using AutoMapper;
using MediatR;
using Shared.Models;
using Domain.Modules.CategoryOfProduct.Commands;
using Domain.Interfaces;
using Shared.Extensions.GeneralExtensions;
using Domain.Modules.CategoryOfProduct.Models;
using Shared.Enums;
using Application.Modules.Base.Commands;
using Domain.Modules.Communication.Generics;

namespace Application.Modules.CategoryOfProduct.Create
{
    [Serializable]
    public class CreateCategoryOfProductHandler : BaseCommandHandler, IRequestHandler<CreateCategoryOfProductCommand, ServiceResponse<OperationResult>>
    {
        public CreateCategoryOfProductHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<ServiceResponse<OperationResult>> Handle(CreateCategoryOfProductCommand command, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(filter='{command.RenderProperties()}', cancellationToken='{cancellationToken}')");
            try
            {

                var model = Mapper.Map<CategoryOfProductModel>(command);
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