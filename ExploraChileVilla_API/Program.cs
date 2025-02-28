using ExploraChileVilla_API;
using ExploraChileVilla_API.Data;
using ExploraChileVilla_API.Repositories;
using ExploraChileVilla_API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregar Servicios

// Configurar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

//Agregar Servicios repositorios para inyectarlo en cualquier parte de la aplicacion
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<INumeroVillaRepository, NumeroVillaRepository>();


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
