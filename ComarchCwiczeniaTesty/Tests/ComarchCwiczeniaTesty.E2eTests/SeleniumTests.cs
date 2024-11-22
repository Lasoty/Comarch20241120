using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ComarchCwiczeniaTesty.E2eTests;

public class SeleniumTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver();

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [TearDown]
    public void Teardown()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Test]
    public void CheckPageTitle()
    {
        driver.Navigate().GoToUrl("https://example.com/");

        Assert.That(driver.Title, Is.EqualTo("Example Domain"), "Tytu³ Strony jest nieprawid³owy!");
    }

    [Test]
    public void CorrectLoginTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        var userNameField = driver.FindElement(By.Id("username"));
        var passwordField = driver.FindElement(By.Id("password"));

        userNameField.SendKeys("tomsmith");
        passwordField.SendKeys("SuperSecretPassword!");

        var loginButton = driver.FindElement(By.XPath("//*[@id=\"login\"]/button/i"));
        loginButton.Click();

        var successMessage = driver.FindElement(By.Id("flash"));
        Assert.That(successMessage.Text, Does.Contain("You logged into a secure area!"));
    }
}