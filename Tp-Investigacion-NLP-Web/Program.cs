using Microsoft.EntityFrameworkCore;
using Tp_Investigacion_NLP_Entidades;
using Tp_Investigacion_NLP_Logica;
using Tp_Investigacion_NLP_Logica.Interfaces;
using Tp_Investigacion_NLP_Logica.Logica;
using Tp_Investigacion_NLP_Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

using var db = new NLPDbContext();
db.Database.Migrate();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<OpenAISettings>(
    builder.Configuration.GetSection("OpenAI"));

builder.Services.Configure<OllamaSettings>(
    builder.Configuration.GetSection("Ollama"));

builder.Services.Configure<GithubModelsSettings>(
    builder.Configuration.GetSection("GithubModels"));

builder.Services.AddHttpClient<OllamaService>(client =>
{
    client.Timeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddHttpClient<GithubModelsService>(client =>
{
    client.Timeout = TimeSpan.FromMinutes(2);
});

builder.Services.AddHttpClient<OpenAIService>(client =>
{
    client.Timeout = TimeSpan.FromMinutes(2);
});

builder.Services.AddScoped<ILLMServiceFactory, LLMServiceFactory>();
builder.Services.AddScoped<IChatLogica, ChatLogica>();
builder.Services.AddDbContext<NLPDbContext>();
builder.Services.AddScoped<IUsuarioLogica, UsuarioLogica>();
builder.Services.AddScoped<IConversacionLogica, ConversacionLogica>();
builder.Services.AddScoped<IMensajeLogica, MensajeLogica>();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

app.UseSession();

app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<ApiExceptionMiddleware>();

// Configure the HTTP request pipeline.
/*
app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();
*/

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
