using GeradorDeTestes.Testes.Interface.Compatilhado;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V137.Audits;
using OpenQA.Selenium.Support.UI;

namespace GeradorDeTestes.Testes.Interface.ModuloMateria;

[TestClass]
[TestCategory("Interface - Materia - Testes")]
public sealed class MateriaInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Materia_Corretamente()
    {
        //Arrange
        //Cadastro de disciplina, necessario para o cadastro de materia(cadastro direto/resumido)
        driver?.Navigate().GoToUrl(Path.Combine(baseUrl, "disciplinas", "Cadastrar"));     
        driver?.FindElement(By.Id("Nome")).SendKeys("DisciplinaTeste");
        driver?.FindElement(By.CssSelector("button[type='submit']")).Click();

        driver?.Navigate().GoToUrl(Path.Combine(baseUrl, "materias", "Cadastrar"));

        //Act
        driver?.FindElement(By.Id("Nome")).SendKeys("MateriaTeste");
        var selectSerie = new SelectElement(driver?.FindElement(By.Id("Serie"))!);  //SelectElement é uma classe do Selenium para manipular elementos <select> em formulários HTML(Pacote Externo/auxiliar ao selenium)
        selectSerie.SelectByIndex(0);   
        
        var selectDisciplina = new SelectElement(driver?.FindElement(By.Id("DisciplinaId"))!);
        selectDisciplina.SelectByText("DisciplinaTeste");
        driver?.FindElement(By.CssSelector("button[type='submit']")).Click();

        //Assert
        var elementCard = driver?.FindElements(By.CssSelector(".card"));
        Assert.AreEqual(1, elementCard?.Count);

    }
}
