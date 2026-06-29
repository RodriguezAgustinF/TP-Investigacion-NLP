# TP Investigación NLP

Prototipo académico de un chatbot construido con ASP.NET Core MVC y .NET 10.

## Arquitectura

- ASP.NET Core MVC y endpoint REST para la interfaz conversacional.
- `Microsoft.Extensions.AI.IChatClient` como abstracción común de proveedores.
- Ollama, OpenAI y GitHub Models intercambiables desde la interfaz.
- Semantic Kernel para publicar funciones nativas como herramientas del modelo.
- Recuperación de conocimiento interno tipo RAG mediante `ConocimientoPlugin`.
- Entity Framework Core y SQL Server para usuarios, conversaciones e historial.
- Logging, límites de contexto, cancelación y autorización por conversación.

## Configuración local

1. Instalar Ollama y descargar el modelo:

   ```powershell
   ollama pull qwen3:8b
   ```

2. Configurar secretos sólo si se usarán proveedores remotos:

   ```powershell
   dotnet user-secrets init --project Tp-Investigacion-NLP-Web
   dotnet user-secrets set "OpenAI:ApiKey" "TU_CLAVE" --project Tp-Investigacion-NLP-Web
   dotnet user-secrets set "GithubModels:Token" "TU_TOKEN" --project Tp-Investigacion-NLP-Web
   ```

3. Ajustar `ConnectionStrings:NLPDatabase` mediante configuración o variable de entorno si SQL Server no está en la instancia local.

4. Ejecutar:

   ```powershell
   dotnet run --project Tp-Investigacion-NLP-Web
   ```

## Verificación

```powershell
dotnet build TP-Investigacion-NLP.slnx
dotnet test TP-Investigacion-NLP.slnx
```

Las claves API y tokens no deben almacenarse en `appsettings.json` ni confirmarse en Git.

## Documentación

- [Arquitectura y componentes](docs/ARQUITECTURA.md)
- [Instalación y configuración](docs/CONFIGURACION.md)
- [API y flujo HTTP](docs/API.md)
- [IA, Semantic Kernel y RAG](docs/IA_Y_RAG.md)
- [Seguridad](docs/SEGURIDAD.md)
- [Desarrollo y pruebas](docs/DESARROLLO_Y_PRUEBAS.md)

Los documentos académicos están en la raíz. La carpeta `docs/` describe el comportamiento real del código de esta rama.
