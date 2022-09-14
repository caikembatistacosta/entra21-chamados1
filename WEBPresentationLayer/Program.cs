
using BLL.Impl;
using BLL.Interfaces;
using DAO;
using DAO.Impl;
using DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WEBPresentationLayer.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("CookieAuthentication")
       .AddCookie("CookieAuthentication", config =>
       {
           config.Cookie.Name = "UserLoginCookie";
           config.LoginPath = "/Login";
           config.AccessDeniedPath = "/Home/Index"; // Adicionar uma página de não autorizado
       });
builder.Services.AddDbContext<DemandasDbContext>(options => {
    options.UseSqlServer("name=ConnectionStrings:DemandaDB");
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IDemandaService, DemandaService>();
builder.Services.AddTransient<IDemandaDAO, DemandaDAO>();
builder.Services.AddHttpClient<DemandaController>();
builder.Services.AddHttpClient<ClienteController>();
builder.Services.AddHttpClient<LoginController>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
