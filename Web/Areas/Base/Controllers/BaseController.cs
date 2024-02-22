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
        protected IDefinitionModel definitionModel { get; set; }
        public IMapper mapper { get; set; }
        public IDbContext dbContext { get; set; }
        public IUserAccessor userAccessor { get; set; }
        protected IMediator mediator { get; set; }
        protected static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public BaseController(IMediator mediator, IMapper mapper, IDbContext dbContext, IUserAccessor userAccessor)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.userAccessor = userAccessor;
        }
    }
}