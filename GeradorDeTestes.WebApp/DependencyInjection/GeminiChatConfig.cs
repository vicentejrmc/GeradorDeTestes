using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.IAGemini;

namespace GeradorDeTestes.WebApp.DependencyInjection;

public static class GeminiChatConfig
{
    public static void AddGeminiChatConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration["GEMINI_API_KEY"];

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new Exception("A variável GEMINI_API_KEY não foi fornecida.");

        var fullEndpoint = string.Concat(
            "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=",
            apiKey
        );

        services.AddHttpClient<GeradorQuestoesGemini>((provider, client) =>
        {
            client.BaseAddress = new Uri(fullEndpoint);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddScoped<IGeradorQuestoes, GeradorQuestoesGemini>();
    }
}
