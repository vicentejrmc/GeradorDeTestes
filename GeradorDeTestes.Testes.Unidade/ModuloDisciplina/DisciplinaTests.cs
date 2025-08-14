using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;

namespace GeradorDeTestes.Testes.Unidade.ModuloDisciplina;

[TestClass]
[TestCategory("Unidade- Disciplina")]
public sealed class DisciplinaTests
{
    private Disciplina? disciplina;


    [TestMethod]
    public void Deve_Adicionar_MateriaA_Disciplina()
    {
        //Arrange
        disciplina = new Disciplina("Variaveis");

        var materiaLogicaDeProgramacao = new Materia(
            "Lógica de Programação",
            SerieMateria.PrimeiroAno,
            disciplina
        );

        //Act
        disciplina.AdicionarMateria(materiaLogicaDeProgramacao);

        //Assert
        var contemMateria = disciplina.Materias.Contains(materiaLogicaDeProgramacao);
        Assert.IsTrue(contemMateria);
    }


    [TestMethod]
    public void Deve_Obter_Questoes_Diciplina_Corretamente()
    {
        //Arrange
        disciplina = new Disciplina("Variaveis");

        var materiaLogicaDeProgramacao = new Materia(
                "Lógica de Programação",
                SerieMateria.PrimeiroAno,
                disciplina
            );

        materiaLogicaDeProgramacao.AdicionarQuestoes([
            new Questao("Qual a função da variavel do tipo int?", materiaLogicaDeProgramacao),
            new Questao("Qual a função da variavel do tipo double?", materiaLogicaDeProgramacao),
            new Questao("Qual a função da variavel do tipo char?", materiaLogicaDeProgramacao),
            new Questao("Qual a função da variavel do tipo string?", materiaLogicaDeProgramacao),
            new Questao("Qual a função da variavel do tipo decimal?", materiaLogicaDeProgramacao)
            ]);

        disciplina.AdicionarMateria(materiaLogicaDeProgramacao);

        //Act
        var questoesSorteadas = disciplina.ObterQuestoesAleatorias(5, SerieMateria.PrimeiroAno);
        List<Questao> questoesEsperadas = questoesSorteadas;

        //Assert
        Assert.AreEqual(5, questoesSorteadas.Count);
        CollectionAssert.IsSubsetOf(questoesSorteadas, questoesEsperadas);
    }
}
