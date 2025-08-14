using GeradorDeTestes.Aplicacao.ModuloDisciplina;
using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;
using Moq;
using Microsoft.Extensions.Logging;

namespace GeradorDeTestes.Testes.Unidade.ModuloDisciplina;

[TestClass]
[TestCategory("Unidade - DisciplinaAppService")]
public sealed class DisciplinaAppServiceTests
{
    private DisciplinaAppService? disciplinaAppService;
    private Mock<IRepositorioDisciplina>? repositorioDisciplinaMock;
    private Mock<IRepositorioMateria>? repositorioMateriaMock;
    private Mock<IRepositorioTeste>? repositorioTesteMock;
    private Mock<IUnitOfWork>? unitOfWorkMock;
    private Mock<ILogger<DisciplinaAppService>>? loggerMock;

    [TestInitialize]
    public void Setup()
    {
        repositorioDisciplinaMock = new Mock<IRepositorioDisciplina>();
        repositorioMateriaMock = new Mock<IRepositorioMateria>();
        repositorioTesteMock = new Mock<IRepositorioTeste>();
        unitOfWorkMock = new Mock<IUnitOfWork>();
        loggerMock = new Mock<ILogger<DisciplinaAppService>>();

        disciplinaAppService = new DisciplinaAppService(
            repositorioDisciplinaMock.Object,
            repositorioMateriaMock.Object,
            repositorioTesteMock.Object,
            unitOfWorkMock.Object,
            loggerMock.Object
        );
    }

    public void Cadastrar_DeveRetornarOk_QuandoDisciplinaValida()
    {
        // Arrange
        var disciplina = new Disciplina("Variaveis");
        var disciplinaTeste = new Disciplina("Teste");

        repositorioDisciplinaMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<Disciplina>() { disciplinaTeste });

        // Act
        var resultado = disciplinaAppService?.Cadastrar(disciplina);

        // Assert
        repositorioDisciplinaMock?.Verify(r => r.Cadastrar(disciplina), Times.Once);
        unitOfWorkMock?.Verify(u => u.Commit(), Times.Once);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsSuccess);
    }

    [TestMethod]
    public void Cadastrar_DeveRetornarFalha_QuandoDisciplinaDuplicada()
    {
        // Arrange
        var disciplina = new Disciplina("Variaveis");
        var disciplinaTeste = new Disciplina("Variaveis");

        repositorioDisciplinaMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<Disciplina>() { disciplinaTeste });

        // Act
        var resultado = disciplinaAppService?.Cadastrar(disciplina);

        // Assert
        repositorioDisciplinaMock?.Verify(r => r.Cadastrar(disciplina), Times.Never);
        unitOfWorkMock?.Verify(u => u.Commit(), Times.Never);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsFailed);
    }

    [TestMethod]
    public void Cadastrar_DeveRetornarFalha_QuandoExcecaoFLancada()
    {
        // Arrange
        var disciplina = new Disciplina("Vetores");

        repositorioDisciplinaMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<Disciplina>());

        unitOfWorkMock?
            .Setup(r => r.Commit())
            .Throws(new Exception("Erro inesperado"));

        // Act
        var resultado = disciplinaAppService?.Cadastrar(disciplina);

        // Assert
        unitOfWorkMock?.Verify(u => u.Rollback(), Times.Once);
        Assert.IsNotNull(resultado);

        var mensagemErro = resultado.Errors.First().Message;

        Assert.AreEqual("Ocorreu um erro interno do servidor", mensagemErro);
        Assert.IsTrue(resultado.IsFailed);
    }
}
