using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Testes.Integracao.Compatilhado;

namespace GeradorDeTestes.Testes.Integracao.ModuloDisciplina;

//Padrao AAA 1-Arrange. 2-Action. 3.Assert)

[TestClass]
[TestCategory("Integração - Disciplina")]
public sealed class RepositorioDisciplinaOrmTests : TesteFixture
{
    [TestMethod]
    public void CadastrarDisciplinaCorretamente()
    {
        //Ararnge
        var disciplina = new Disciplina("Programação Sequencial");

        // Act
        repositorioDisciplina.Cadastrar(disciplina);
        dbContext.SaveChanges();

        //Assert
        var registro = repositorioDisciplina.SelecionarRegistroPorId(disciplina.Id);
        Assert.AreEqual(disciplina, registro);
    }

    [TestMethod]
    public void SelecionarDisciplinasCorretamente()
    {
        //1 - Arrange.
        var disciplina = new Disciplina("Programação Sequencial");
        var disciplina1 = new Disciplina("Programação Extruturada");
        var disciplina2 = new Disciplina("Programação Orientada a Objetos");
        repositorioDisciplina.Cadastrar(disciplina);
        repositorioDisciplina.Cadastrar(disciplina1);
        repositorioDisciplina.Cadastrar(disciplina2);

        dbContext.SaveChanges();

        List<Disciplina> listDisciplinas = [disciplina, disciplina1, disciplina2];

        //2 - Act.
        var registrosSelecionados = repositorioDisciplina.SelecionarRegistros();

        //3.Assert
        //Verifica se a lista dos selecionados é igual a lista que fois salva para o teste
        CollectionAssert.Equals(listDisciplinas, registrosSelecionados);
    }

    [TestMethod]
    public void EditarDisciplinaCorretamente()
    {
        //Arrange
        var disciplina = new Disciplina("Vetores");
        repositorioDisciplina.Cadastrar(disciplina);
        dbContext.SaveChanges();

        var disciplinaEditada = new Disciplina("Variaveis");

        //Act
        var etidadoComSucesso = repositorioDisciplina.Editar(disciplina.Id, disciplinaEditada);
        dbContext.SaveChanges();

        //Assert
        var registroSelecionado = repositorioDisciplina.SelecionarRegistroPorId(disciplina.Id);

        Assert.IsTrue(etidadoComSucesso);
        // verifica se o resultado da edição é true;
        Assert.AreEqual(disciplina, registroSelecionado);
        // verifica se os registros são iguais levando em contaa  referencia original dentro do contexto(método) do teste
    }

    [TestMethod]
    public void ExcluirDisciplinaCorretamente()
    {
        //Arrange
        var disciplina = new Disciplina("Vetores");
        repositorioDisciplina.Cadastrar(disciplina);
        dbContext.SaveChanges();

        //Act
        var conseguiuExcluir = repositorioDisciplina.Excluir(disciplina.Id);
        dbContext.SaveChanges();

        //Assert
        var resgistroSelecionado = repositorioDisciplina.SelecionarRegistroPorId(disciplina.Id);
        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(resgistroSelecionado);
    }
}
