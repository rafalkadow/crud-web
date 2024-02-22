﻿using Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Domain.Modules.SignOut.Consts;

namespace Web.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class UserAuthorization : AuthorizeAttribute, IAuthorizationFilter
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public OperationEnum OperationType { get; set; }

		public string ClaimValue { get; set; }

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			logger.Info($"OnAuthorization(context)");
			var principal = context.HttpContext.User as ClaimsPrincipal;

			if (!principal.Identity.IsAuthenticated)
			{
				context.Result = new RedirectResult("~/" + SignOutConsts.Url);
				return;
			}
		}
	}
}