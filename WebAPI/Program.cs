using Application;
using Persistence;
using Shared;
using WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Serilog.Core;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfraestructure(builder.Configuration);
builder.Services.AddSharedInfraestructure(builder.Configuration);

builder.Services.AddHttpClient("PorcentajesApiClient", client =>
{
    client.BaseAddress = new Uri("https://659b57add565feee2daaf810.mockapi.io/api/v1/");
});

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddApiVersioningExtensions();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración del servicio de logging


Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging(bldr =>
{
    bldr.AddSerilog();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UserErrroHandlingMiddleware();

app.MapControllers();

app.Run();
