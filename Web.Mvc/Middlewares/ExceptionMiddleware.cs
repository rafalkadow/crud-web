using Microsoft.AspNetCore.Http.Extensions;

namespace Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError($"Invoke(exception={ex})");
                logger.LogError($"Invoke(exception.Message={ex.Message})");
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var url = httpContext.Request.GetEncodedUrl();
                httpContext.Response.Redirect("/error/500/Url?errorUrl=" + url);
                logger.LogError($"Invoke(exception.url={url})");
                await httpContext.Response.WriteAsync(ex.ToString());
                throw;
            }
        }
    }
}