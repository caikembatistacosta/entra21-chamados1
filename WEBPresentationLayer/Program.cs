
using BLL.Impl;
using BLL.Interfaces;
using DAO;
using DAO.Impl;
using DAO.Interfaces;
using Entities.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WEBPresentationLayer.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.AccessDeniedPath = "/Login/Index";
    });
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Adm", p => p.RequireRole(NivelDeAcesso.Adm.ToString()));
});

builder.Services.AddDbContext<DemandasDbContext>(options =>
{
    options.UseSqlServer("name=ConnectionStrings:Default");
});
// Add services to the container.
builder.Services.AddControllersWithViews();


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
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(op =>
{
    op.WithOrigins("https://localhost:7202/");
    op.AllowAnyMethod();
    op.AllowAnyHeader();
    op.AllowAnyOrigin();
});
app.UseCookiePolicy();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
