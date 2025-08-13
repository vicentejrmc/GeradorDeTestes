using FizzWare.NBuilder;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Testes.Integracao.Compatilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Testes.Integracao.ModutoTestes;

[TestClass]
[TestCategory("Integração - Testes")]
public sealed class RepositorioTestesOrmTests : TesteFixture
{
    [TestMethod]
    public void CadastrarTesteCorretamente()
    {
        // Arrange
        var disciplina = Builder<Disciplina>.CreateNew()
            .With(d => d.Nome = "Matemática")
            .Persist();

        var materia = Builder<Materia>.CreateNew()
            .With(m => m.Nome = "Quatro Operações")
            .With(m => m.Disciplina = disciplina)
            .Persist();

        var questoes = Builder<Questao>.CreateListOfSize(5)
            .All()
            .With(q => q.Materia = materia)
            .Persist()
            .ToList();

        var teste = new Teste(
            titulo: "Teste de Matemática",
            recuperacao: false,
            qteQuestoes: 5,
            disciplina,
            serieMateria: materia.Serie,
            materia
        );

        teste.Questoes = questoes;

        // Act
        repositorioTeste?.Cadastrar(teste);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioTeste?.SelecionarRegistroPorId(teste.Id);

        Assert.AreEqual(teste, registroSelecionado);
    }

    [TestMethod]
    public void ExcluirTesteCorretamente()
    {
        // Arrange
        var disciplina = Builder<Disciplina>.CreateNew()
            .With(d => d.Nome = "Matemática")
            .Persist();

        var materia = Builder<Materia>.CreateNew()
            .With(m => m.Nome = "Quatro Operações")
            .With(m => m.Disciplina = disciplina)
            .Persist();

        var questoes = Builder<Questao>.CreateListOfSize(5)
            .All()
            .With(q => q.Materia = materia)
            .Persist()
            .ToList();

        var teste = new Teste(
            titulo: "Teste de Matemática",
            recuperacao: false,
            qteQuestoes: 5,
            disciplina,
            serieMateria: materia.Serie,
            materia
        );

        teste.Questoes = questoes;

        repositorioTeste?.Cadastrar(teste);
        dbContext?.SaveChanges();

        // Act
        var conseguiuExcluir = repositorioTeste?.Excluir(teste.Id);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioTeste?.SelecionarRegistroPorId(teste.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(registroSelecionado);
    }

    [TestMethod]
    public void SelecionarTestesCorretamente()
    {
        var disciplina = Builder<Disciplina>.CreateNew()
            .With(d => d.Nome = "Matemática")
            .Persist();

        var materia = Builder<Materia>.CreateNew()
            .With(m => m.Nome = "Quatro Operações")
            .With(m => m.Disciplina = disciplina)
            .Persist();

        var questoes = Builder<Questao>.CreateListOfSize(5)
            .All()
            .With(q => q.Materia = materia)
            .Persist()
            .ToList();

        var teste = new Teste(
            titulo: "Teste de Matemática",
            recuperacao: false,
            qteQuestoes: 5,
            disciplina,
            serieMateria: materia.Serie,
            materia
        );

        teste.Questoes = questoes;

        var teste2 = new Teste(
            titulo: "Teste de Matemática de Recuperação",
            recuperacao: true,
            qteQuestoes: 5,
            disciplina,
            serieMateria: materia.Serie,
            materia: null
        );

        teste2.Questoes = questoes;

        List<Teste> registrosEsperados = [teste, teste2];

        repositorioTeste?.CadastrarEntidades(registrosEsperados);
        dbContext?.SaveChanges();

        // Act
        var registrosRecebidos = repositorioTeste?.SelecionarRegistros();

        // Assert
        var registrosEsperadosOrdenados = registrosEsperados
            .OrderBy(x => x.Titulo).ToList();

        CollectionAssert.AreEqual(registrosEsperadosOrdenados, registrosRecebidos);
    }
}
