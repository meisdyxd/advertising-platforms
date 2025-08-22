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
    .AddSwagger()
    .AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseSwaggerCustom();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();