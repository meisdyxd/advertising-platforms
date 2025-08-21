using AdvertisingPlatforms.Infrastructure.PlatformsTree;
using AdvertisingPlatforms.Application.Interfaces;
using AdvertisingPlatforms.Application.Services;
using AdvertisingPlatforms.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
});

builder.Services.AddSingleton<IPlatformsTree, PlatformsTree>();
builder.Services.AddScoped<PlatformService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();