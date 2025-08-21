using GeradorDeTestes.Testes.Interface.Compatilhado;
using OpenQA.Selenium;

namespace GeradorDeTestes.Testes.Interface.ModuloDisciplina;
[TestClass]
[TestCategory("Interface - Disciplina - Testes")]
public sealed class DisciplinaInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Disciplina_Corretamente()
    {
        //Arrange
        driver.Navigate().GoToUrl(Path.Combine(baseUrl, "disciplinas"));

        var elemento = driver.FindElement(By.CssSelector("a[data-se='btnCadastrar']"));
        elemento?.Click();

        //Act
        driver?.FindElement(By.Id("Nome")).SendKeys("DisciplinaTeste");
        driver?.FindElement(By.CssSelector("button[type='submit']")).Click();

        //Assert
        var elementCard = driver?.FindElements(By.CssSelector(".card")); // Observer a diferentça para encontrar um elemento (FindElement), para uma coleção de elementos (FindElements)
        Assert.AreEqual(1, elementCard?.Count);
    }
}
