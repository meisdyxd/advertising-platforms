using Serilog;

namespace AdvertisingPlatforms.API.Registrations;

public static class SerilogRegistration
{
    public static void Execute(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Seq(configuration.GetConnectionString("Seq")
                ?? throw new Exception("Seq connection string is empty"))
            .CreateLogger();
    }
}
