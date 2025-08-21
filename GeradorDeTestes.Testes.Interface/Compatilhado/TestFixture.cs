using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace GeradorDeTestes.Testes.Interface.Compatilhado;

[TestClass]
public abstract class TestFixture
{
    protected static IWebDriver? driver;

    [TestInitialize]
    public void ConfigureTest()
    {
        InitializeWebDriver();
    }

    [TestCleanup]
    public void EncerrarTestes()
    {
        EncerrarWebDriver();
    }

    private static void InitializeWebDriver()
    {
        driver = new EdgeDriver();
    }

    private static void EncerrarWebDriver()
    {
        driver?.Quit();
        driver?.Dispose();
    }

}
