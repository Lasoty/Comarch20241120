using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczeniaTesty.UnitTests
{

    [TestFixture]
    public class CalculatorServiceTests
    {
        CalculatorService cut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

        }

        [SetUp]
        public void Setup()
        {
            cut = new CalculatorService();
        }


        [TearDown]
        public void Teardown()
        {
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }

        [Test]
        public void AddShouldReturnCorrectSumValue()
        {
            // Arrange
            int x = 2, y = 2;
            int expected = 4;
            int actual = 0;

            // Act
            actual = cut.Add(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SubtractShouldReturnCorrectValue()
        {
            // Arrange
            int x = 2, y = 2;
            int expected = 0;
            int actual = 0;

            // Act
            actual = cut.Subtract(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MultiplyShouldReturnCorrectValue()
        {
            // Arrange
            int x = 2, y = 2;
            int expected = 4;
            int actual = 4;

            // Act
            actual = cut.Multiply(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void DividyShouldReturnCorrectValue()
        {
            // Arrange
            int x = 2, y = 2;
            float expected = 1;
            float actual = 0;

            // Act
            actual = cut.Dividy(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }


    }
}
