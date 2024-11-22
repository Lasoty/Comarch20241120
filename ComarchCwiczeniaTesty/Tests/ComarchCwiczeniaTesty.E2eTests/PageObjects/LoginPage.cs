using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ComarchCwiczeniaTesty.E2eTests.PageObjects;

public class LoginPage
{
    private readonly IWebDriver driver;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

    }

    private IWebElement UsernameField => driver.FindElement(By.Id("username"));
    private IWebElement PasswordField => driver.FindElement(By.Id("password"));
    private IWebElement LoginButton => driver.FindElement(By.XPath("//*[@id=\"login\"]/button/i"));
    private IWebElement ErrorMessage => driver.FindElement(By.Id("flash"));

    public LoginPage Open()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
        return this;
    }

    public void EnterUserName(string username)
    {
        UsernameField.SendKeys(username);
    }

    public void EnterPassword(string password)
    {
        PasswordField.SendKeys(password);
    }

    public void ClickLoginButton()
    {
        LoginButton.Click();
    }

    public bool IsErrorMessageDisplayed()
    {
        return ErrorMessage.Text.Contains("invalid");
    }

    public bool ProceedLogin(string username, string password)
    {
        EnterUserName(username);
        EnterPassword(password);
        ClickLoginButton();
        return !IsErrorMessageDisplayed();
    }

}