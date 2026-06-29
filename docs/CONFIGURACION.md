# Instalación y configuración

## Requisitos

- .NET SDK 10.
- SQL Server accesible desde la aplicación.
- Ollama para ejecución local, o credenciales para un proveedor remoto.

## Base de datos

La conexión predeterminada está en `ConnectionStrings:NLPDatabase`. Puede sobrescribirse sin modificar archivos:

```powershell
$env:ConnectionStrings__NLPDatabase="Server=.;Database=TP-NLP;Trusted_Connection=True;TrustServerCertificate=True"
```

Al iniciar, la aplicación ejecuta las migraciones pendientes con `Database.Migrate()`.

## Ollama

```powershell
ollama pull qwen3:8b
ollama serve
```

Configuración predeterminada:

```json
"Ollama": {
  "BaseUrl": "http://localhost:11434",
  "Model": "qwen3:8b"
}
```

## GitHub Models

Guardar el token mediante User Secrets:

```powershell
dotnet user-secrets set "GithubModels:Token" "TU_TOKEN" --project Tp-Investigacion-NLP-Web
```

Las propiedades no sensibles `BaseUrl` y `Model` permanecen en `appsettings.json`.

## Prioridad de configuración

ASP.NET Core combina las fuentes habituales. Una variable de entorno utiliza `__` para representar `:`:

```powershell
$env:GithubModels__Token="TU_TOKEN"
$env:Ollama__Model="qwen3:8b"
```

No se deben guardar claves en `appsettings.json`, código fuente, capturas, documentos o commits.

## Ejecución

```powershell
dotnet restore
dotnet run --project Tp-Investigacion-NLP-Web
```

La URL local exacta se informa en la consola y también puede consultarse en `Properties/launchSettings.json`.

## Problemas comunes

| Síntoma | Causa probable | Solución |
|---|---|---|
| Ollama no responde | Servicio detenido o modelo ausente | Ejecutar `ollama serve` y `ollama pull qwen3:8b`. |
| Falta `GITHUB_MODELS_TOKEN` | Token no configurado | Guardarlo con User Secrets y reiniciar. |
| Error SQL Server | Instancia o conexión incorrecta | Sobrescribir `ConnectionStrings__NLPDatabase`. |
| Herramienta no invocada | El modelo no soporta bien function calling | Probar otro modelo o formular una consulta explícita sobre el TP. |
