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
            path.StartsWith("/css") ||
            path.StartsWith("/js") ||
            path.StartsWith("/lib");

        if (rutaPublica)
        {
            await _next(context);
            return;
        }

        int? usuarioId = context.Session.GetInt32("UsuarioId");

        if (usuarioId == null)
        {
            context.Response.Redirect($"/Usuario/Login?returnUrl={path}");
            return;
        }

        await _next(context);
    }
}
