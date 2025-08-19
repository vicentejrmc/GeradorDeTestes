using Serilog;
using Serilog.Events;

namespace GeradorDeTestes.WebApp.DependencyInjection;

public static class SerilogConfig
{
    public static void AddSerilogConfig(this IServiceCollection services, ILoggingBuilder logging, IConfiguration configuration)
    {
        var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var caminhoArquivoLogs = Path.Combine(caminhoAppData, "GeradorDeTestes", "erro.log");
        var licenseKey = configuration["NEWRELIC_LICENSE_KEY"];

        if (string.IsNullOrWhiteSpace(licenseKey))
            throw new Exception("A variável NEWRELIC_LICENSE_KEY não foi encontrada.");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error)
            .WriteTo.NewRelicLogs(
                endpointUrl: "http://log-api.newrelic.com/log/v1",
                applicationName: "gerador-de-testes-app",
                licenseKey: licenseKey
            )
            .CreateLogger();

        logging.ClearProviders();
        services.AddSerilog();
    }
}
