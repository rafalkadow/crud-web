using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Base.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Web.Areas.Base.Controllers
{
    [Serializable]
    public class BaseController : Controller
    {
        protected IDefinitionModel? definitionModel { get; set; }
        protected IMapper mapper { get; set; }
        protected IDbContext dbContext { get; set; }
        protected IUserAccessor userAccessor { get; set; }
        protected IMediator mediator { get; set; }
        protected static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Base constructor class.
        /// </summary>
        public BaseController(IMediator mediator, IMapper mapper, IDbContext dbContext, IUserAccessor userAccessor)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.userAccessor = userAccessor;
        }
    }
}