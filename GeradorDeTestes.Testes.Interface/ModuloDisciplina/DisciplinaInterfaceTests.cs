using GeradorDeTestes.Testes.Interface.Compatilhado;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GeradorDeTestes.Testes.Interface.ModuloDisciplina;
[TestClass]
[TestCategory("Interface - Disciplina - Testes")]
public sealed class DisciplinaInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Disciplina_Corretamente()
    {
        //Arrange
        driver?.Navigate().GoToUrl(Path.Combine(baseUrl, "disciplinas"));

        var elemento = driver?.FindElement(By.CssSelector("a[data-se='btnCadastrar']"));
        elemento?.Click();

        //Act
        driver?.FindElement(By.Id("Nome")).SendKeys("DisciplinaTeste");
        driver?.FindElement(By.CssSelector("button[type='submit']")).Click();

        //Assert
        var elementCard = driver?.FindElements(By.CssSelector(".card")); // Observer a diferentça para encontrar um elemento (FindElement), para uma coleção de elementos (FindElements)
        Assert.AreEqual(1, elementCard?.Count);
    }

    [TestMethod]
    public void Deve_Editar_Disciplina_Corretamente()
    {
        //Arrange
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); // Espera até 5 segundos para que o elemento esteja presente na página para evitar falsos negativos

        driver?.Navigate().GoToUrl(Path.Combine(baseUrl, "disciplinas"));
        driver?.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Click();
        driver?.FindElement(By.Id("Nome")).SendKeys("DisciplinaTeste");
        driver?.FindElement(By.CssSelector("button[type='submit']")).Click();

        wait.Until(d => d.FindElements(By.CssSelector(".card")).Count == 1);

        driver?.FindElement(By.CssSelector(".card"))
            .FindElement(By.CssSelector("a[title='Edição")).Click();

        //Act
        driver?.FindElement(By.Id("Nome")).SendKeys(" Editada");
        driver?.FindElement(By.CssSelector("button[type='submit']")).Click();

        //Assert
        wait.Until(d => d.FindElements(By.CssSelector(".card")).Count == 1); // Espera até que haja apenas um card na página, indicando que a edição foi concluída para evitar falsos negativos
        Assert.IsTrue(driver?.PageSource.Contains("DisciplinaTeste Editada"));
    }
}
