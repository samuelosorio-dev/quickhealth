using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using QuickHealth.Application.Mappings;
using QuickHealth.Application.Ports.In;
using QuickHealth.Application.Ports.Out;
using QuickHealth.Application.UseCases;
using QuickHealth.Application.Validators;
using QuickHealth.Infrastructure.Adapters.Out.Persistence.Context;
using QuickHealth.Infrastructure.Adapters.Out.Persistence.Repositories;
using QuickHealth.Infrastructure.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapsterConfig = TypeAdapterConfig.GlobalSettings;
MapsterConfig.Configure(mapsterConfig);

// FluentValidation
builder.Services.AddScoped<IValidator<CrearPacienteRequestDTO>, PacienteValidator>();
builder.Services.AddScoped<IValidator<ActualizarSignosVitalesRequestDTO>, ActualizarPacienteValidator>();


// EF Core
builder.Services.AddDbContext<QuickHealthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Inyección de dependencias (Manejo de puertos de salida)

// Con SQL server
builder.Services.AddScoped<IRepositorioPaciente, RepositorioPaciente>();

// Con memoria
//builder.Services.AddSingleton<IRepositorioPaciente, RepositorioPacienteMemoria>();


//Inyeccion del puerto de entrada para el UseCase
builder.Services.AddScoped<ICasosUsoPaciente, ServicioPaciente>();


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
