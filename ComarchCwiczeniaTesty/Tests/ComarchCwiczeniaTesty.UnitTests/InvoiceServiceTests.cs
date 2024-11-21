﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczeniaTesty.Invoicing;
using FluentAssertions;
using Moq;

namespace ComarchCwiczeniaTesty.UnitTests;

[TestFixture]
internal class InvoiceServiceTests
{
    private Mock<ITaxService> taxServiceMock;
    private Mock<IDiscountService> discountServiceMock;
    private InvoiceService invoiceService;

    [SetUp]
    public void Setup()
    {
        taxServiceMock = new Mock<ITaxService>();
        discountServiceMock = new Mock<IDiscountService>();

        invoiceService = new InvoiceService(taxServiceMock.Object, discountServiceMock.Object);
    }

    [Test]
    public void CalculateTotalShouldRun()
    {
        invoiceService.CalculateTotal(3, "");
        Assert.Pass();
    }

    [Test]
    public void CalculateTotalWhenCalledVerifiesTaxServiceGetTaxIsCalled()
    {
        // Arrange

        var amount = 100m;
        var customerType = "Regular";

        discountServiceMock.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Returns(10m);

        taxServiceMock.Setup(x => x.GetTax(It.IsAny<decimal>())).Verifiable();
        taxServiceMock.Setup(x => x.GetTax(It.IsAny<decimal>())).Returns(5m);

        // Act

        invoiceService.CalculateTotal(amount, customerType);

        // Assert
        taxServiceMock.Verify(x => x.GetTax(It.IsAny<decimal>()), Times.Once);
    }

    [Test]
    public void CalculateTotalShouldReturnsExpectedValue()
    {
        // Arrange
        var amount = 100m;
        var customerType = "Regular";

        discountServiceMock.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Returns(10m);
        taxServiceMock.Setup(x => x.GetTax(It.IsInRange<decimal>(0, 10, Moq.Range.Inclusive))).Returns(3m);
        taxServiceMock.Setup(x => x.GetTax(It.IsInRange<decimal>(11, 100, Moq.Range.Inclusive))).Returns(5m);
        taxServiceMock.Setup(x => x.GetTax(It.IsInRange<decimal>(101, decimal.MaxValue, Moq.Range.Inclusive))).Returns(1m);
        
        // Act
        var actual = invoiceService.CalculateTotal(amount, customerType);

        // Assert
        actual.Should().Be(95m);
    }
}