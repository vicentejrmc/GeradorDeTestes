using DotNet.Testcontainers.Containers;
using FizzWare.NBuilder;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace GeradorDeTestes.Testes.Integracao.Compatilhado;

[TestClass]
public abstract class TesteFixture
{
    protected GeradorDeTestesDbContext? dbContext;

    protected RepositorioTesteOrm? repositorioTeste;
    protected RepositorioQuestaoOrm? repositorioQuestao;
    protected RepositorioMateriaOrm? repositorioMateria;
    protected RepositorioDisciplinaOrm? repositorioDisciplina;

    private static IDatabaseContainer? dbContainer;

    [AssemblyInitialize]
    public static async Task Setup(TestContext _)
    {
        dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .WithName("gerador-de-testes-container")
            .WithCleanUp(true)
            .Build();

        await InicializarContainerBancoDadosAsync(dbContainer);
    }

    [AssemblyCleanup]
    public static async Task Teardown()
    {
        await PararContainerBancoDadosAsync();
    }

    [TestInitialize]
    public void ConfigurarTestes()
    {
        if (dbContainer is null)
            throw new ArgumentNullException("O Banco de dados não foi inicializado.");

        dbContext = TesteDbContextFactory.CriarDbContext(dbContainer.GetConnectionString());

        ConfigurarTabelas(dbContext);

        repositorioTeste = new RepositorioTesteOrm(dbContext);
        repositorioQuestao = new RepositorioQuestaoOrm(dbContext);
        repositorioDisciplina = new RepositorioDisciplinaOrm(dbContext);
        repositorioMateria = new RepositorioMateriaOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Disciplina>(repositorioDisciplina.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Disciplina>>(repositorioDisciplina.CadastrarEntidades);

        BuilderSetup.SetCreatePersistenceMethod<Materia>(repositorioMateria.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Materia>>(repositorioMateria.CadastrarEntidades);

        BuilderSetup.SetCreatePersistenceMethod<Questao>(repositorioQuestao.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Questao>>(repositorioQuestao.CadastrarEntidades);

        BuilderSetup.SetCreatePersistenceMethod<Teste>(repositorioTeste.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Teste>>(repositorioTeste.CadastrarEntidades);
    }

    private static void ConfigurarTabelas(GeradorDeTestesDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        dbContext.Testes.RemoveRange(dbContext.Testes);
        dbContext.Questoes.RemoveRange(dbContext.Questoes);
        dbContext.Materias.RemoveRange(dbContext.Materias);
        dbContext.Disciplinas.RemoveRange(dbContext.Disciplinas);

        dbContext.SaveChanges();
    }

    private static async Task InicializarContainerBancoDadosAsync(IDatabaseContainer dbContainer)
    {
        await dbContainer.StartAsync();
    }

    private static async Task PararContainerBancoDadosAsync()
    {
        if (dbContainer is null)
            throw new ArgumentNullException("O Banco de dados não foi inicializado.");

        await dbContainer.StopAsync();
        await dbContainer.DisposeAsync();
    }
}
