using System;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TaskManager.CustomExceptions
{
  public class MiddlewareExceptionHandler
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<MiddlewareExceptionHandler> _logger;

    public MiddlewareExceptionHandler(RequestDelegate next, ILogger<MiddlewareExceptionHandler> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (System.Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, System.Exception exception)
    {
        if(exception is NotFoundException)
        {
            return responseJSON(context, exception, (int)HttpStatusCode.NotFound);
        }
        if(exception is BadRequestException)
        {
            return responseJSON(context, exception, (int)HttpStatusCode.BadRequest);
        }

        return responseJSON(context, exception, (int)HttpStatusCode.InternalServerError);
    }

    private static Task responseJSON(HttpContext context, System.Exception exception, int code)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(new ErrorResponse()
        {
            TimeStamps = DateTime.Now,
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Path = context.Request.Path,
            exception = exception.GetType().Name
        }.ToString());
    }
  }
}