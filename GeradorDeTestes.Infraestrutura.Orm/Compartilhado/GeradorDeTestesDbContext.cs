using Microsoft.EntityFrameworkCore;


namespace GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
public class GeradorDeTestesDbContext : DbContext
{
   // public DbSet<Entidade> Entidades { get; set; }

    public GeradorDeTestesDbContext(DbContextOptions optionsDb) : base(optionsDb){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(GeradorDeTestesDbContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }

    public void Commit()
    {
        SaveChanges();
    }

    public void RollBack()
    {
        foreach(var entrada in ChangeTracker.Entries())
        {
            switch(entrada.State)
            {
                case EntityState.Added:
                    entrada.State = EntityState.Unchanged; break;

                case EntityState.Modified:
                    entrada.State = EntityState.Unchanged; break;
                
                case EntityState.Deleted:
                    entrada.State = EntityState.Unchanged; break;

            }
        }
    }
}
