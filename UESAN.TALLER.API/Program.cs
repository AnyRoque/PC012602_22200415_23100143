using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using UESAN.TALLER.CORE.Core.Interfaces;
using UESAN.TALLER.CORE.Infrastructure.Data;
using UESAN.TALLER.CORE.Infrastructure.Repositories;
using UESAN.TALLER.CORE.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión desde appsettings.json
var cnx = builder.Configuration.GetConnectionString("DevConnection");
builder.Services.AddDbContext<TallerDbContext>(options =>
    options.UseSqlServer(cnx));

// Inyección de dependencias (Pregunta 5)
builder.Services.AddScoped<IOrdenServicioRepository, OrdenServicioRepository>();
builder.Services.AddScoped<IOrdenServicioService, OrdenServicioService>();

builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
