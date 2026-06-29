namespace Tp_Investigacion_NLP_Web.Middleware;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionMiddleware> _logger;

    public ApiExceptionMiddleware(
        RequestDelegate next,
        ILogger<ApiExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado procesando {Path}", context.Request.Path);

            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.StatusCode = ex switch
                {
                    ArgumentException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    error = context.Response.StatusCode == StatusCodes.Status500InternalServerError
                        ? "No se pudo procesar la solicitud."
                        : ex.Message
                });

                return;
            }

            throw;
        }
    }
}
