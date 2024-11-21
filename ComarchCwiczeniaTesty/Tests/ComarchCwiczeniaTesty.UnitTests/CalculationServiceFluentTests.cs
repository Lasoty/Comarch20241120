using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace ComarchCwiczeniaTesty.UnitTests;

[TestFixture]
internal class CalculationServiceFluentTests
{
    private CalculatorService calculatorService;

    [SetUp]
    public void SetUp()
    {
        calculatorService = new CalculatorService();
    }

    [Test]
    public void ConcatenateStringShouldReturnMergedValue()
    {
        // Arrange
        string string1 = "John";
        string string2 = "Doe";

        // Act
        string actual = calculatorService.ConcatenateString(string1, string2);

        // Assert
        actual.Should()
            .NotBeNullOrWhiteSpace().And
            .StartWith(string1).And
            .EndWith(string2).And
            .Contain(" ").And
            .HaveLength(string1.Length + string2.Length + 1);

        char.IsUpper(actual[0]).Should().BeTrue("Should be start with big letter.");
    }

    [Test]
    public void CalculateBirthDayShouldReturnCorrectDate()
    {
        // Arrange
        int age = 30;
        int birthDay = 15;
        int birthMonth = 6;
        DateTime expectedDate = new DateTime(DateTime.Now.Year - age, birthMonth, birthDay);

        // Act
        DateTime actual = calculatorService.CalculateBirthDay(age, birthDay, birthMonth);

        // Assert
        actual.Should().Be(expectedDate);
    }

    [Test]
    public void CalculateBirthDayShouldThrowExceptionForInvalidDay()
    {
        // Arrange
        int age = 30;
        int birthDay = 32;
        int birthMonth = 6;

        // Act
        Action act = () => calculatorService.CalculateBirthDay(age, birthDay, birthMonth);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void CalculateBirthDayShouldThrowExceptionForInvalidMonth()
    {
        // Arrange
        int age = 30;
        int birthDay = 15;
        int birthMonth = 13;

        // Act
        Action act = () => calculatorService.CalculateBirthDay(age, birthDay, birthMonth);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void CalculateBirthDayShouldThrowExceptionForNegativeAge()
    {
        // Arrange
        int age = -1;
        int birthDay = 15;
        int birthMonth = 6;

        // Act
        Action act = () => calculatorService.CalculateBirthDay(age, birthDay, birthMonth);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void CalculateBirthDayShouldHandleLeapYear()
    {
        // Arrange
        int age = 4;
        int birthDay = 29;
        int birthMonth = 2;
        DateTime expectedDate = new DateTime(DateTime.Now.Year - age, birthMonth, birthDay);

        // Act
        DateTime actual = calculatorService.CalculateBirthDay(age, birthDay, birthMonth);

        // Assert
        actual.Should().Be(expectedDate);
    }

    [Test]
    public void CalculateBirthDayShouldHandleNonLeapYear()
    {
        // Arrange
        int age = 1;
        int birthDay = 28;
        int birthMonth = 2;
        DateTime expectedDate = new DateTime(DateTime.Now.Year - age, birthMonth, birthDay);

        // Act
        DateTime actual = calculatorService.CalculateBirthDay(age, birthDay, birthMonth);

        // Assert
        actual.Should().Be(expectedDate);
    }
}
