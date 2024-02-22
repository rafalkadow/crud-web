using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Product.Commands;
using Domain.Modules.Product.Models;
using Shared.Enums;
using Shared.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Application.Modules.Base.Commands;

namespace Application.Modules.Product.Update
{
    [Serializable]
    public class UpdateProductHandler : BaseCommandHandler, IRequestHandler<UpdateProductCommand, OperationResult>
    {
        public UpdateProductHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<OperationResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(filter='{command.RenderProperties()}', cancellationToken='{cancellationToken}')");
            try
            {
                var model = await DbContext
                    .GetQueryable<ProductModel>()
                    .FirstOrDefaultAsync(x => x.Id == command.Id);

                if (model == null || model.Id == Guid.Empty)
                {
                    return new OperationResult(false);
                }
                ModifiedOrderSet(model, command);
                await DbContext.UpdatePropertiesAsync(model, command, UserAccessor);
                return new OperationResult(model, OperationEnum.Update);
            }
            catch (Exception ex)
            {
                logger.Error($"Handle(ex='{ex.ToString()}')");
                throw;
            }
        }
    }
}