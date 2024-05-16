using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Web.Api.Controllers.Base
{
    [Serializable]
    public class BaseController : Controller
    {
        protected IMediator mediator { get; set; }
        protected static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Base constructor class.
        /// </summary>
        public BaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}