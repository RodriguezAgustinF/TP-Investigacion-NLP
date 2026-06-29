# Desarrollo, pruebas y mantenimiento

## Comandos habituales

```powershell
dotnet restore
dotnet build TP-Investigacion-NLP.slnx
dotnet test TP-Investigacion-NLP.slnx
dotnet run --project Tp-Investigacion-NLP-Web
```

## Migraciones

Ejecutar desde la raíz:

```powershell
dotnet ef migrations add NombreMigracion --project Tp-Investigacion-NLP-Entidades --startup-project Tp-Investigacion-NLP-Web
dotnet ef database update --project Tp-Investigacion-NLP-Entidades --startup-project Tp-Investigacion-NLP-Web
```

No se deben editar manualmente los archivos generados en `Migrations` salvo que se comprenda el impacto sobre el snapshot.

## Pruebas actuales

`ConocimientoPluginTests` verifica:

- recuperación de contenido sobre RAG;
- búsqueda tolerante a acentos;
- respuesta segura cuando no existe información.

## Pruebas recomendadas

- autorización: un usuario no puede escribir o eliminar conversaciones ajenas;
- controladores: estados 400, 401 y 404;
- fábrica: credenciales ausentes y selección correcta del proveedor;
- integración EF Core con una base aislada;
- contrato del endpoint JSON;
- funcionamiento con un cliente de IA simulado;
- límite de historial y propagación de cancelación;
- prueba integral opcional con Ollama.

Las pruebas unitarias no deben depender de una API paga ni de una conexión real a SQL Server.

## Convenciones

- La Web no debe contener lógica de proveedores.
- La lógica no debe depender de controladores o sesiones HTTP.
- Los secretos nunca forman parte de DTOs, logs o mensajes de error.
- Las funciones expuestas al modelo deben tener nombres y descripciones precisas.
- Los cambios en contratos públicos deben actualizar esta documentación y sus pruebas.

## Checklist antes de entregar

- [ ] `dotnet build` termina sin advertencias.
- [ ] `dotnet test` aprueba todas las pruebas.
- [ ] No existen secretos en `git diff` ni en el historial compartido.
- [ ] Ollama o el proveedor elegido responde desde la interfaz.
- [ ] Registro, login, creación y eliminación de chats funcionan.
- [ ] Las consultas sobre el TP activan la recuperación cuando corresponde.
- [ ] Word, PPT y README describen las mismas tecnologías que el código.
