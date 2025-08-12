using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Testes.Integracao.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GeradorDeTestes.Testes.Integracao.Compatilhado;
public static class TesteDbContextFactory
{
    public static GeradorDeTestesDbContext CriarDbContext()
    {
        //configurando conecção com bando de dados
        var configuracao = CriarConfiguracao();

        var connectionStrig = configuracao["SQL_CONNECTION_STRING"];
        var options = new DbContextOptionsBuilder<GeradorDeTestesDbContext>()
            .UseNpgsql(connectionStrig)
            .Options;

        var dbContext = new GeradorDeTestesDbContext(options);

        dbContext.Database.EnsureDeleted(); // limpa o banco de dados para não haver conflitos entres testes
        dbContext.Database.EnsureCreated();  //Cria banco de dados

        return dbContext;
    }

    public static IConfiguration CriarConfiguracao()
    {
        //configurando qual projeto que será executado por meio do Assembly
        var assembly = typeof(RepositorioDisciplinaOrmTests).Assembly;

        return new ConfigurationBuilder()
            .AddUserSecrets(assembly)
            .Build();
    }
}
