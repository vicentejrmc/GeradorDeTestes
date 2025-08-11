using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GeradorDeTestes.Testes.Integracao.ModuloDisciplina;

[TestClass]
[TestCategory("Integração - Disciplina")]
public sealed class RepositorioDisciplinaOrmTests
{
    private GeradorDeTestesDbContext dbContext;
    private RepositorioDisciplinaOrm repositorioDisciplina;

    [TestMethod]
    public void CadastrarRegistroCorretamente()
    {
        //configurando qual projeto que será executado por meio do Assembly
        var assembly = typeof(RepositorioDisciplinaOrmTests).Assembly;
        var configuracao = new ConfigurationBuilder()
            .AddUserSecrets(assembly)
            .Build();

        //configurando conecção com bando de dados
        var connectionStrig = configuracao["SQL_CONNECTION_STRING"];
        var options = new DbContextOptionsBuilder<GeradorDeTestesDbContext>()
            .UseNpgsql(connectionStrig)
            .Options;

        dbContext = new GeradorDeTestesDbContext(options);
        repositorioDisciplina = new RepositorioDisciplinaOrm(dbContext);

        //criando disciplina/Cadastrando/Selecionado por Id
        var disciplina = new Disciplina("Programação Sequencial");
        repositorioDisciplina.Cadastrar(disciplina);
        dbContext.SaveChanges();
        var registro = repositorioDisciplina.SelecionarRegistroPorId(disciplina.Id);
        
        //verifica se o objeto selecionado é igual ao cadastrado para teste
        Assert.AreEqual(disciplina, registro);
    }

    [TestMethod]
    public void SelecionarRegistrosCorretamente()
    {

    }
}
