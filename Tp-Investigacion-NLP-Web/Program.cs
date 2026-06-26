using Microsoft.EntityFrameworkCore;
using Tp_Investigacion_NLP_Entidades;
using Tp_Investigacion_NLP_Logica.Interfaces;
using Tp_Investigacion_NLP_Logica.Logica;
using Tp_Investigacion_NLP_Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

using var db = new NLPDbContext();
db.Database.Migrate();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
