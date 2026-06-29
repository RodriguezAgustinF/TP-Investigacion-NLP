namespace Tp_Investigacion_NLP_Web.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? path = context.Request.Path.Value?.ToLower();

        bool rutaPublica =
            path == "/" ||
            path == "/home" ||
            path == "/home/index" ||
            path == "/usuario/login" ||
            path == "/usuario/registro" ||
            EsArchivoPublico(path);

        if (rutaPublica)
        {
            await _next(context);
            return;
        }

        int? usuarioId = context.Session.GetInt32("UsuarioId");

        if (usuarioId == null)
        {
            if (EsApi(path))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("""
                {
                    "error": "Usuario no autenticado."
                }
                """);

                return;
            }

            context.Response.Redirect($"/Usuario/Login?returnUrl={path}");
            return;
        }

        await _next(context);
    }

    private bool EsApi(string? path)
    {
        return path != null && path.StartsWith("/api");
    }

    private bool EsArchivoPublico(string? path)
    {
        if (path == null)
            return false;

        return path.StartsWith("/css") ||
               path.StartsWith("/js") ||
               path.StartsWith("/lib") ||
               path.StartsWith("/favicon");
    }
}
