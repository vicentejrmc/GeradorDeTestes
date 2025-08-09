using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Pdf.GeradorDeTestesPdf;

namespace GeradorDeTestes.WebApp.DependencyInjection;

public static class QuestPDFConfig
{
    public static void AddQuestPDFConfig(this IServiceCollection services)
    {
        services.AddScoped<IGeradorTeste, GeradorTestePdf>();
    }
}