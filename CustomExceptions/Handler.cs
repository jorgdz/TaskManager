using TaskManager.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
 
namespace TaskManager.CustomExceptions
{
  public static class Handler
  {
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<MiddlewareExceptionHandler>();
    }
  }
}
