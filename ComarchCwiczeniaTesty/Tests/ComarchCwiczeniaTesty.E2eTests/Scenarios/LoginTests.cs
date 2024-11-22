using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczeniaTesty.E2eTests.PageObjects;
using FluentAssertions;
using OpenQA.Selenium;

namespace ComarchCwiczeniaTesty.E2eTests.Scenarios;

[TestFixture]
public class LoginTests : TestBase
{
    [Test]
    public void LoginWithInvalidCredentialsShouldShowErrorMessage()
    {
        var loginPage = new LoginPage(driver);
        loginPage.ProceedLogin("tomsmith", "ZleHasło").Should().BeFalse();
        //new LoginPage(driver).ProceedLogin("tomsmith", "ZleHasło").Should().BeFalse();
    }
}