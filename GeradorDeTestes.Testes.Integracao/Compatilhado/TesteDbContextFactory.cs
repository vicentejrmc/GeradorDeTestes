using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

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
}
