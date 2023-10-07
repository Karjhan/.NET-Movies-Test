using System.Text.Json;
using API.Errors;

namespace API.Middlewares;

public class ExceptionMiddleware
{
    public RequestDelegate Next { get; set; }

    public ILogger<ExceptionMiddleware> Logger { get; set; }

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        Next = next;
        Logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (Exception e)
        {
            Logger.LogError(e, e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            APIException response = new APIException(500);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}