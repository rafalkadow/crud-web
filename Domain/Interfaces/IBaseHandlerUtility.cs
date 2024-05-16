using Domain.Modules.Base.Models;
using Domain.Modules.Communication.Generics;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Interfaces
{
	public interface IBaseHandlerUtility
    {
        public Task<ServiceResponse<OperationResult>> CommandActionAsync<T>(OperationModel operation, Func<T, bool> condition = null) where T : class, IEntity;
    }
}