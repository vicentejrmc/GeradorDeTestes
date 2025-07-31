using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.WebApp.Orm;

public static class DataBaseOperations
{
    public static void ApplyMigrations(this IHost app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GeradorDeTestesDbContext>();

        dbContext.Database.Migrate();
    }
}
