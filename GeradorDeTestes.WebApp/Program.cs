using GeradorDeTestes.Aplicacao.ModuloDisciplina;
using GeradorDeTestes.Aplicacao.ModuloMateria;
using GeradorDeTestes.Aplicacao.ModuloQuestao;
using GeradorDeTestes.Aplicacao.ModuloTeste;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;
using GeradorDeTestes.WebApp.ActionFilters;
using GeradorDeTestes.WebApp.DependencyInjection;
using GeradorDeTestes.WebApp.Orm;

namespace GeradorDeTestes.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //AppServices
        builder.Services.AddScoped<DisciplinaAppService>();
        builder.Services.AddScoped<MateriaAppService>();
        builder.Services.AddScoped<QuestaoAppService>();
        builder.Services.AddScoped<TesteAppService>();

        //Repositorios
        builder.Services.AddScoped<IRepositorioDisciplina, RepositorioDisciplinaOrm>();
        builder.Services.AddScoped<IRepositorioMateria, RepositorioMateriaOrm>();
        builder.Services.AddScoped<IRepositorioQuestao, RepositorioQuestaoOrm>();
        builder.Services.AddScoped<IRepositorioTeste, RepositorioTesteOrm>();

        builder.Services.AddEntityFrameworkConfig(builder.Configuration);

        builder.Services.AddSerilogConfig(builder.Logging, builder.Configuration);

        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ValidarModelAttribute>();
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.ApplyMigrations();

            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/erro");
        }

        app.UseAntiforgery();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapDefaultControllerRoute();
        app.Run();
    }
}
