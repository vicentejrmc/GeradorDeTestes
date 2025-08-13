using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Testes.Integracao.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;

namespace GeradorDeTestes.Testes.Integracao.Compatilhado;
public class TesteDbContextFactory
{
    public static GeradorDeTestesDbContext CriarDbContext(string connectionString)
    {
        var options = new DbContextOptionsBuilder<GeradorDeTestesDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        var dbContext = new GeradorDeTestesDbContext(options);

        return dbContext;
    }


    //public TesteDbContextFactory()
    //{
    //    var options = new DbContextOptionsBuilder<GeradorDeTestesDbContext>()
    //        .UseNpgsql()

    //    container = new PostgreSqlBuilder()
    //        .WithImage("postgres:16")
    //        .WithName("gerador-de-testes-testsdb-container")
    //        .Build();
    //}

    //public async Task InicializarAsync()
    //{
    //    await container.StartAsync();
    //}

    //public async Task EncerrarAsync()
    //{
    //    await container.StopAsync();
    //    await container.DisposeAsync();
    //}


    //public static GeradorDeTestesDbContext CriarDbContext(string connectionString)
    //{
    //    var options = new DbContextOptionsBuilder<GeradorDeTestesDbContext>()
    //        .UseNpgsql(connectionString)
    //        .Options;

    //    var dbContext = new GeradorDeTestesDbContext(options);

    //    return dbContext;
    //}
}
