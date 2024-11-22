using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace ComarchCwiczeniaTesty.E2eTests.Scenarios;

public abstract class TestBase
{
    protected IWebDriver driver;

    [SetUp]
    public virtual void Setup()
    {
        var optionsChrome = new ChromeOptions();
        //optionsChrome.AddArgument("headless");
        //optionsChrome.AddArgument("--disable-gpu");
        //optionsChrome.AddArgument("--no-sandbox");

        //var optionsFirefox = new FirefoxOptions();
        //optionsFirefox.AddArgument("--headless");

        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver(optionsChrome);

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [TearDown]
    public virtual void Teardown()
    {
        driver.Quit();
        driver.Dispose();
    }
}