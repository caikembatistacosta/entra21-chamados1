using AutoMapper;
using BLL.Impl;
using BLL.Interfaces;
using DAO;
using DAO.Impl;
using DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DemandasDbContext>(options => {
    options.UseSqlServer("name=ConnectionStrings:DemandaDB");
});
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IClienteDAO, ClienteDAO>();
builder.Services.AddTransient<IDemandaService, DemandaService>();
builder.Services.AddTransient<IDemandaDAO, DemandaDAO>();
builder.Services.AddTransient<IFuncionarioDAO, FuncionarioDAO>();
builder.Services.AddTransient<IFuncionarioService, FuncionarioService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
