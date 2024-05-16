using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Account.Queries;
using Domain.Modules.SignIn.Commands;
using Shared.Models;
using Application.Seeder;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Application.Modules.Base.Commands;
using Application.Modules.Account.Queries;
using Domain.Modules.Communication.Generics;

namespace Application.Modules.SignIn.Commands
{
    [Serializable]
    public class SignInHandler : BaseCommandHandler, IRequestHandler<SignInCommand, ServiceResponse<OperationResult>>
    {
        public SignInHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<ServiceResponse<OperationResult>> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(filter='{command.RenderProperties()}', cancellationToken='{cancellationToken}')");
            try
            {
                await new DataSeeder().SeedDataOnApplication(DbContext, Mapper, UserAccessor, logger);

                var commandHandler = new GetAccountQueryByIdHandler(DbContext, Mapper, UserAccessor);
                var accountFind = await commandHandler.Handle(new GetAccountQueryById(command.SignInEmail), CancellationToken.None);
                if (accountFind == null || accountFind.Id == Guid.Empty)
                {
                    return new ServiceResponse<OperationResult>(new OperationResult(false));
                }
                var messageUrl = "";
                return new ServiceResponse<OperationResult>(new OperationResult(true) { EntityId = accountFind.Id,  Message = messageUrl });
            }
            catch (Exception ex)
            {
                logger.Error($"Handle(ex='{ex.ToString()}')");
				throw;
			}
        }
    }
}