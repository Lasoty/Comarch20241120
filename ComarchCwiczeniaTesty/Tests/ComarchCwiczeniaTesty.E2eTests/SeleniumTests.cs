using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ComarchCwiczeniaTesty.E2eTests;

public class SeleniumTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        var optionsChrome = new ChromeOptions();
        optionsChrome.AddArgument("headless");
        optionsChrome.AddArgument("--disable-gpu");
        optionsChrome.AddArgument("--no-sandbox");
        optionsChrome.AddArgument("--window-size=1920,1080");
        optionsChrome.AddArgument("--incognito");
        optionsChrome.AddArgument("--start-maximized"); //nie dzia³a w trybie headless
        optionsChrome.AddArgument("--ignore-certificate-errors");
        optionsChrome.AddArgument("--disable-popup-blocking");
        optionsChrome.AddArgument("--lang=pl-PL");
        optionsChrome.AddArgument("--allow-insecure-localhost");
        
        
        //var optionsFirefox = new FirefoxOptions();
        //optionsFirefox.AddArgument("--headless");

        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver(optionsChrome);

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

    [Test]
    public void HandleJavaScriptAlertTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

        var alertButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[1]/button"));
        alertButton.Click();

        var alert = driver.SwitchTo().Alert();

        alert.Text.Should().Contain("I am a JS Alert");

        alert.Accept();

        var resultMessage = driver.FindElement(By.Id("result"));
        resultMessage.Text.Should().Contain("You successfully clicked an alert");
    }

    [Test]
    public void HandleJavaScriptConfirmPositiveTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

        var confirmButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[2]/button"));
        confirmButton.Click();
        
        var alert = driver.SwitchTo().Alert();
        alert.Text.Should().Contain("I am a JS Confirm");

        alert.Accept();
        
        var resultMessage = driver.FindElement(By.Id("result"));
        resultMessage.Text.Should().Contain("You clicked: Ok");
    }

    [Test]
    public void HandleJavaScriptConfirmNegativeTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

        var confirmButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[2]/button"));
        confirmButton.Click();

        var alert = driver.SwitchTo().Alert();
        alert.Text.Should().Contain("I am a JS Confirm");

        alert.Dismiss();

        var resultMessage = driver.FindElement(By.Id("result"));
        resultMessage.Text.Should().Contain("You clicked: Cancel");
    }


    [Test]
    public void HandleJavaScriptPromptPositiveTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

        var confirmButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[3]/button"));
        confirmButton.Click();

        var alert = driver.SwitchTo().Alert();
        alert.Text.Should().Contain("I am a JS prompt");

        alert.SendKeys("Test");
        alert.Accept();

        var resultMessage = driver.FindElement(By.Id("result"));
        resultMessage.Text.Should().Contain("You entered: Test");
    }

    [Test]
    public void DynamicLoadingTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
        var startButton = driver.FindElement(By.XPath("//*[@id=\"start\"]/button"));
        startButton.Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        
        var loadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finish")));
        loadedElement.Text.Should().Contain("Hello World!");
    }

    [Test]
    public void FileUploadTest()
    {
        var tt = Environment.CurrentDirectory;

        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/upload");
        var fileInput = driver.FindElement(By.Id("file-upload"));

        var filePath = @"Assets\UploadTest.txt";
        var fileInfo = new FileInfo(filePath);
        fileInfo.Exists.Should().BeTrue();

        fileInput.SendKeys(fileInfo.FullName);

        var submitButton = driver.FindElement(By.Id("file-submit"));
        submitButton.Click();

        var infoMsg = driver.FindElement(By.Id("uploaded-files"));
        infoMsg.Text.Should().Contain(fileInfo.Name);
    }


}