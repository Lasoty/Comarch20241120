using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

    [Test]
    public void IncorrectLoginTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        var usernameField = driver.FindElement(By.Id("username"));
        var passwordField = driver.FindElement(By.Id("password"));
        
        usernameField.SendKeys("wrongUser");
        passwordField.SendKeys("wrongPassword");
        
        var loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();
        
        var errorMessage = driver.FindElement(By.CssSelector(".flash.error"));
        Assert.That(errorMessage.Text, Does.Contain("Your username is invalid!"), "Niepoprawne dane logowania nie wywo³a³y b³êdu.");
    }

    [Test]
    public void CheckboxesTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/checkboxes");

        var checkbox = driver.FindElement(By.XPath("//*[@id=\"checkboxes\"]/input[2]"));
        bool isSelected = checkbox.Selected;

        isSelected.Should().BeTrue("Checkbox powinien byæ zaznaczony");
    }

    [Test]
    public void DropDownTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dropdown");

        var dropdown = driver.FindElement(By.Id("dropdown"));

        var selectElement = new SelectElement(dropdown);
        selectElement.SelectByValue("1");

        selectElement.SelectedOption.Text.Should().Contain("Option 1");

    }


}