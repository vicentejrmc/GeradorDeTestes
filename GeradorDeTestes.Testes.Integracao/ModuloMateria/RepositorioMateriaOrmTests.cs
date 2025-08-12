using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
using GeradorDeTestes.Testes.Integracao.Compatilhado;
using FizzWare.NBuilder;

namespace GeradorDeTestes.Testes.Integracao.ModuloMateria;

[TestClass]
[TestCategory("Integração - Materia")]
public sealed class RepositorioMateriaOrmTests
{
    private GeradorDeTestesDbContext dbContext;
    private RepositorioMateriaOrm repositorioMateria;
    private RepositorioDisciplinaOrm repositorioDisciplina;

    [TestInitialize]
    public void ConfigurarTestes()
    {
        dbContext = TesteDbContextFactory.CriarDbContext();
        repositorioDisciplina = new RepositorioDisciplinaOrm(dbContext);
        repositorioMateria = new RepositorioMateriaOrm(dbContext);

        //usando NBuilder conseguimos configurar o "cadastro" de entidades necessarias para a entidade que está sendo testada.
        //sem a necessidade de criar ou cadastra-la dentro do teste da entidade em questão.
        BuilderSetup.SetCreatePersistenceMethod<Disciplina>(repositorioDisciplina.Cadastrar);
    }

    [TestMethod]
    public void CadastrarMateriaCorretamente()
    {
        //Arrange
        var disciplina = Builder<Disciplina>.CreateNew().Persist();

        var materia = new Materia("Arrays", SerieMateria.PrimeiroAno, disciplina);

        //Act
        repositorioMateria.Cadastrar(materia);
        dbContext.SaveChanges();

        //Assert
        var materiaSelecionada = repositorioMateria.SelecionarRegistroPorId(materia.Id);
        Assert.AreEqual(materia, materiaSelecionada);
    }

    [TestMethod]
    public void SelecionarMateriasCorretamente()
    {
        //1 - Arrange.
        var disciplina = Builder<Disciplina>.CreateNew().Persist();

        var materia = new Materia("Vetores", SerieMateria.PrimeiroAno, disciplina);
        var materia1 = new Materia("Variaveis", SerieMateria.PrimeiroAno, disciplina);
        var materia2 = new Materia("Arrays", SerieMateria.PrimeiroAno, disciplina);

        repositorioMateria.Cadastrar(materia);
        repositorioMateria.Cadastrar(materia1);
        repositorioMateria.Cadastrar(materia2);
        dbContext.SaveChanges();

        List<Materia> listMaterias = [materia, materia1, materia2];
        var materiasEsperadas = listMaterias.OrderBy(m => m.Nome).ToList();

        //2 - Act.
        var materiasRecebidas = repositorioMateria.SelecionarRegistros();

        //Assert
        CollectionAssert.AreEqual(materiasEsperadas, materiasRecebidas);
    }

    [TestMethod]
    public void EditarMateriaCorretamente()
    {
        //Arrange
        var disciplina = Builder<Disciplina>.CreateNew().Persist();

        var materia = new Materia("Vetores", SerieMateria.PrimeiroAno, disciplina);
        repositorioMateria.Cadastrar(materia);
        dbContext.SaveChanges();

        var materiaEditada = new Materia("Variaveis", SerieMateria.SegundoAno, disciplina);

        //Act
        var etidadoComSucesso = repositorioMateria.Editar(materia.Id, materiaEditada);
        dbContext.SaveChanges();

        //Assert
        var materiaSelecionada = repositorioMateria.SelecionarRegistroPorId(materia.Id);

        Assert.IsTrue(etidadoComSucesso); 
        Assert.AreEqual(materia, materiaSelecionada);
        
    }

    [TestMethod]
    public void ExcluirMateriaCorretamente()
    {
        //Arrange
        var disciplina = Builder<Disciplina>.CreateNew().Persist();

        var materia = new Materia("Vetores", SerieMateria.TerceiroAno, disciplina);
        repositorioMateria.Cadastrar(materia);
        dbContext.SaveChanges();

        //Act
        var conseguiuExcluir = repositorioMateria.Excluir(materia.Id);
        dbContext.SaveChanges();

        //Assert
        var resgistroSelecionado = repositorioMateria.SelecionarRegistroPorId(materia.Id);
        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(resgistroSelecionado);
    }
}
