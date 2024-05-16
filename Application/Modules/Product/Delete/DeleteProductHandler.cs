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
using Domain.Modules.Communication.Generics;

namespace Application.Modules.Product.Delete
{
    [Serializable]
    public class DeleteProductHandler : BaseCommandHandler, IRequestHandler<DeleteProductCommand, ServiceResponse<OperationResult>>
    {
        public DeleteProductHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<ServiceResponse<OperationResult>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(filter='{command.RenderProperties()}', cancellationToken='{cancellationToken}')");
            try
            {
                foreach (var guidId in command.GuidList)
                {
                    var model = await DbContext
                        .GetQueryable<ProductModel>()
                        .FirstOrDefaultAsync(x => x.Id == guidId);

                    if (model == null || model.Id == Guid.Empty)
                    {
                        return new ServiceResponse<OperationResult>(new OperationResult(false));
                    }
                    await DbContext.DeleteAsync(model);
                    await DbContext.SaveChangesAsync();
                }
                return new ServiceResponse<OperationResult>(new OperationResult(true, OperationEnum.Delete));
            }
            catch (Exception ex)
            {
                logger.Error($"Handle(ex='{ex.ToString()}')");
                throw;
            }
        }
    }
}