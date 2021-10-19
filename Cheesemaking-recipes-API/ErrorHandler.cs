using Cheesemaking_recipes_API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Cheesemaking_recipes_API
{
    public class ErrorHandler : IMiddleware
    {
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(ILogger<ErrorHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (InputCountException e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(e.Message);
            }
            catch (BadRequestException e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Błąd serwera");
            }
        }
    }
}
