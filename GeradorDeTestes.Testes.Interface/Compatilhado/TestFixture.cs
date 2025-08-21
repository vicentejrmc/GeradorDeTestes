using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.ComponentModel;
using System.Data.Common;

namespace GeradorDeTestes.Testes.Interface.Compatilhado;

[TestClass]
public abstract class TestFixture
{
    protected static IWebDriver? driver;
    protected static GeradorDeTestesDbContext? dbContext;
    protected static string baseUrl = "https://localhost:7056";
    private static string connectionString ="Host=localhost;Port=5432;Database=gerador-de-testes-db;Username=postgres;Password=MinhaSenhaFraca;";


    [TestInitialize]
    public void ConfigureTest()
    {
        dbContext = TesteDbContextFactory.CriarDbContext(connectionString);

        ConfigurarTabelas(dbContext);

        InitializeWebDriver();
    }

    [TestCleanup]
    public void EncerrarTestes()
    {
        EncerrarWebDriver();
    }

    private static void InitializeWebDriver()
    {
        driver = new EdgeDriver();
    }

    private static void EncerrarWebDriver()
    {
        driver?.Quit();
        driver?.Dispose();
    }

    private static void ConfigurarTabelas(GeradorDeTestesDbContext dbcontext)
    {
        dbcontext.Database.EnsureCreated();

        dbcontext.Testes.RemoveRange(dbcontext.Testes);
        dbcontext.Questoes.RemoveRange(dbcontext.Questoes);
        dbcontext.Materias.RemoveRange(dbcontext.Materias);
        dbcontext.Disciplinas.RemoveRange(dbcontext.Disciplinas);

        dbcontext.SaveChanges();
    }
}
