using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WebApi.Services.LoggerServices;

namespace WebApi.Middlewares;

public class CustomExceptionMiddleware
{
    readonly RequestDelegate _next;
    readonly IFileLoggerService _filelogger;

    public CustomExceptionMiddleware(RequestDelegate next, IFileLoggerService fileLogger)
    {
        _next = next;
        _filelogger = fileLogger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch watch = Stopwatch.StartNew();
        string requestMethod = context.Request.Method, requestPath = context.Request.Path;

        try
        {
            _filelogger.Log($"[{DateTime.Now:yyyy.MM.dd}] [Request] HTTP{requestMethod} {requestPath}");

            await _next(context);

            watch.Stop();

            _filelogger.Log($"[{DateTime.Now:yyyy.MM.dd}] [Response] HTTP{requestMethod} {requestPath} responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds}ms");
        }
        catch (Exception ex)
        {
            watch.Stop();

            _filelogger.Log($"[{DateTime.Now:yyyy.MM.dd}] [Error] HTTP{requestMethod} {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds}ms. Message: {ex.Message}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
        }
    }
}

public static class CustomExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}