using FizzWare.NBuilder;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Testes.Integracao.Compatilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Testes.Integracao.ModuloQuestoes;
public sealed class RepositorioQuestoesOrmTests
{
    [TestClass]
    [TestCategory("Integração - Questões")]
    public sealed class RepositorioQuestaoOrmTests : TesteFixture
    {
        [TestMethod]
        public void CadastrarQuestaoCorretamente()
        {
            // Arrange
            var disciplina = Builder<Disciplina>.CreateNew()
                .With(d => d.Nome = "Lógica de Programação")
                .Persist();

            var materia = Builder<Materia>.CreateNew()
                .With(m => m.Nome = "Variaveis")
                .With(m => m.Disciplina = disciplina)
                .Persist();

            var questao = new Questao("Qual tipo usado para numeros inteiros em C#?", materia);

            // Act
            repositorioQuestao?.Cadastrar(questao);
            dbContext?.SaveChanges();

            // Assert
            var registroSelecionado = repositorioQuestao?.SelecionarRegistroPorId(questao.Id);

            Assert.AreEqual(questao, registroSelecionado);
        }

        [TestMethod]
        public void EditarQuestaoCorretamente()
        {
            // Arrange
            var disciplina = Builder<Disciplina>.CreateNew()
                .With(d => d.Nome = "Lógica de Programação")
                .Persist();

            var materia = Builder<Materia>.CreateNew()
                .With(m => m.Nome = "Variaveis")
                .With(m => m.Disciplina = disciplina)
                .Persist();

            var questao = new Questao("Qual tipo usado para numeros inteiros em C#?", materia);

            repositorioQuestao?.Cadastrar(questao);
            dbContext?.SaveChanges();

            var questaoEditada = new Questao("Qual tipo usado para Caracteres em C#??", materia);

            // Act
            var conseguiuEditar = repositorioQuestao?.Editar(questao.Id, questaoEditada);
            dbContext?.SaveChanges();

            // Assert
            var registroSelecionado = repositorioQuestao?.SelecionarRegistroPorId(questao.Id);

            Assert.IsTrue(conseguiuEditar);
            Assert.AreEqual(questao, registroSelecionado);
        }

        [TestMethod]
        public void ExcluirQuestaoCorretamente()
        {
            // Arrange
            var disciplina = Builder<Disciplina>.CreateNew()
                .With(d => d.Nome = "Lógica de Programação")
                .Persist();

            var materia = Builder<Materia>.CreateNew()
                .With(m => m.Nome = "Variaveis")
                .With(m => m.Disciplina = disciplina)
                .Persist();

            var questao = new Questao("Qual tipo usado para Caracteres em C#", materia);

            repositorioQuestao?.Cadastrar(questao);
            dbContext?.SaveChanges();

            // Act
            var conseguiuExcluir = repositorioQuestao?.Excluir(questao.Id);
            dbContext?.SaveChanges();

            // Assert
            var registroSelecionado = repositorioQuestao?.SelecionarRegistroPorId(questao.Id);

            Assert.IsTrue(conseguiuExcluir);
            Assert.IsNull(registroSelecionado);
        }

        [TestMethod]
        public void SelecionarQuestoesCorretamente()
        {
            // Arrange
            var disciplina = Builder<Disciplina>.CreateNew()
                .With(d => d.Nome = "Lógica de Programação")
                .Persist();

            var materias = Builder<Materia>.CreateListOfSize(3)
                .All()
                .With(m => m.Disciplina = disciplina)
                .Persist();

            var questao = new Questao("Qual tipo usado para Caracteres em C#?", materias[0]);
            var questao2 = new Questao("Qual tipo usado para numeros inteiros em C#?", materias[1]);
            var questao3 = new Questao("Qual tipo usado para palavras em C#", materias[2]);

            List<Questao> registrosEsperados = [questao, questao2, questao3];

            repositorioQuestao?.CadastrarEntidades(registrosEsperados);
            dbContext?.SaveChanges();

            // Act
            var registrosRecebidos = repositorioQuestao?.SelecionarRegistros();

            // Assert
            var registrosEsperadosOrdenados = registrosEsperados
                .OrderBy(x => x.Enunciado).ToList();

            CollectionAssert.AreEqual(registrosEsperadosOrdenados, registrosRecebidos);
        }
    }
}
