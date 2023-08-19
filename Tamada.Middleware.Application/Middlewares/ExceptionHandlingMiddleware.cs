using System.Text.Json; 
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public sealed class ExceptionHandlingMiddleware  : IMiddleware
    {
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.logger = logger;
    }
       public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(JsonSerializer.Serialize(e));
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = exception switch
            {
                BadRequestException _ => StatusCodes.Status400BadRequest,
                NotFoundException _ => StatusCodes.Status404NotFound,
                UnAuthorizedException _ => StatusCodes.Status401Unauthorized,
                ForbiddenException _ => StatusCodes.Status403Forbidden,
                UnProcessedEntityException _ => StatusCodes.Status422UnprocessableEntity,
                ServerErrorException _ => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new
            {
                Message = exception.Message,
                Success = false,
                StatusCode = httpContext.Response.StatusCode,
                
            };

           

            if (httpContext.Response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                response = new { Message = "I would like to help you, how about chat with an agent",
                Success = false,
                StatusCode = httpContext.Response.StatusCode,
                };
            }

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

     

}

