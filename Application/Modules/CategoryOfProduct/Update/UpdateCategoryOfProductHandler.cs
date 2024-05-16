using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.CategoryOfProduct.Commands;
using Domain.Modules.CategoryOfProduct.Models;
using Shared.Enums;
using Shared.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Application.Modules.Base.Commands;
using Domain.Modules.Communication.Generics;

namespace Application.Modules.CategoryOfProduct.Update
{
    [Serializable]
    public class UpdateCategoryOfProductHandler : BaseCommandHandler, IRequestHandler<UpdateCategoryOfProductCommand, ServiceResponse<OperationResult>>
    {
        public UpdateCategoryOfProductHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<ServiceResponse<OperationResult>> Handle(UpdateCategoryOfProductCommand command, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(filter='{command.RenderProperties()}', cancellationToken='{cancellationToken}')");
            try
            {
                var model = await DbContext
                    .GetQueryable<CategoryOfProductModel>()
                    .FirstOrDefaultAsync(x => x.Id == command.Id);

                if (model == null || model.Id == Guid.Empty)
                {
                    return new ServiceResponse<OperationResult>(new OperationResult(false));
                }
                ModifiedOrderSet(model, command);
                await DbContext.UpdatePropertiesAsync(model, command, UserAccessor);
                return new ServiceResponse<OperationResult>(new OperationResult(model, OperationEnum.Update));
            }
            catch (Exception ex)
            {
                logger.Error($"Handle(ex='{ex.ToString()}')");
                throw;
            }
        }
    }
}