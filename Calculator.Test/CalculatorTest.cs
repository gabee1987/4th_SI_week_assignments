﻿using NUnit.Framework;

namespace Calculator.Tests
{
    [TestFixture]
    public class CalculatorTest
    {

        ICalculator sut;

        [SetUp]
        public void TestSetup()
        {
            sut = new Calculator();
        }

        [Test]
        public void ShouldAddTwoNumbers()
        {
            int expectedResult = sut.Add(7, 8);
            Assert.That(expectedResult, Is.EqualTo(15));
        }

        [Test]
        public void ShouldMulTwoNumbers()
        {   
            int expectedResult = sut.Mul(7, 8);
            Assert.That(expectedResult, Is.EqualTo(56));
        }

        [TearDown]
        public void TestTearDown()
        {
            sut = null;
        }
    }
}
