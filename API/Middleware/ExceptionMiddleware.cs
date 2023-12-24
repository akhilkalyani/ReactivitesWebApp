using System.Net;
using System.Text.Json;
using Application.Core;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }        

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                httpContext.Response.ContentType="application/json";
                httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

                var response=env.IsDevelopment()?
                    new AppException(httpContext.Response.StatusCode,ex.Message,ex.StackTrace?.ToString())
                    :new AppException(httpContext.Response.StatusCode,"Internal server error");
            
                var options=new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                var json=JsonSerializer.Serialize(options);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}