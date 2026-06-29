# Seguridad

## Controles implementados

- Contraseñas almacenadas con `PasswordHasher<Usuario>`.
- Identidad de usuario conservada en sesión del servidor.
- Middleware que restringe rutas privadas.
- Verificación de propiedad antes de escribir en una conversación.
- Límite de 4000 caracteres por mensaje.
- Validación explícita del proveedor solicitado.
- Escape HTML predeterminado de Razor e inserción con `innerText` en JavaScript.
- Secretos fuera de `appsettings.json`.
- Errores internos registrados, pero no expuestos en la respuesta HTTP.
- Cancelación propagada cuando el cliente interrumpe la solicitud.

## Modelo de confianza

Las instrucciones y la salida del modelo no se consideran confiables. Las herramientas disponibles son de sólo lectura y exponen únicamente conocimiento controlado. Nunca se debe permitir que el modelo ejecute SQL, comandos o llamadas arbitrarias proporcionadas por el usuario.

## Secretos

Para desarrollo se utilizan User Secrets. En producción deben usarse variables de entorno o un almacén administrado. Si una clave fue confirmada en Git, quitarla del archivo no es suficiente: debe revocarse y reemplazarse.

## Riesgos pendientes

- No existe rate limiting por usuario o IP.
- La sesión usa almacenamiento en memoria y no se comparte entre instancias.
- No hay bloqueo progresivo ante intentos de login.
- No se aplican políticas de moderación específicas.
- No hay cuotas por proveedor o usuario.
- Se ejecutan migraciones automáticamente al iniciar.
- Falta una política explícita de retención y eliminación de conversaciones.

## Recomendaciones para producción

1. Migrar la autenticación a ASP.NET Core Identity.
2. Configurar cookies `Secure`, `HttpOnly`, `SameSite` y expiración según el entorno.
3. Aplicar rate limiting al login y a `/api/chat/enviar`.
4. Usar un almacenamiento distribuido para sesiones.
5. Aplicar migraciones en el despliegue, no al arrancar cada instancia.
6. Añadir métricas, auditoría y alertas sin registrar prompts sensibles.
7. Incorporar moderación y pruebas de prompt injection.
8. Definir límites de tokens, presupuesto y tiempo por proveedor.
