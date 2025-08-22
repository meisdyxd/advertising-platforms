using AdvertisingPlatforms.API.Middlewares;
using AdvertisingPlatforms.API.Registrations;
using AdvertisingPlatforms.Application;
using AdvertisingPlatforms.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

SerilogRegistration.Execute(builder.Configuration);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddSerilog()
    .AddSwagger()
    .AddControllers();

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseSwaggerCustom();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();