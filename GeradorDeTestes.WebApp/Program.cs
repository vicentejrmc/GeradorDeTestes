using GeradorDeTestes.Aplicacao.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
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

        if(builder.Environment.IsDevelopment())
        {
            //AppServices
            builder.Services.AddScoped<DisciplinaAppService>();

            //Repositorios
            builder.Services.AddScoped<IRepositorioDisciplina, RepositorioDisciplinaOrm>();
            builder.Services.AddScoped<IRepositorioMateria, RepositorioMateriaOrm>();
            builder.Services.AddScoped<IRepositorioTeste, RepositorioTesteOrm>();

            builder.Services.AddEntityFrameworkConfig(builder.Configuration);
        }

        builder.Services.AddSerilogConfig(builder.Logging);

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
