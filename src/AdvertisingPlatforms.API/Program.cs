using AdvertisingPlatforms.API.Middlewares;
using AdvertisingPlatforms.API.Registrations;
using AdvertisingPlatforms.Application;
using AdvertisingPlatforms.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddSwagger()
    .AddControllers();

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseSwaggerCustom();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();