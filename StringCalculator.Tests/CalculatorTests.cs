using NUnit.Framework;
using System;

namespace StringCalculator.Tests
{
    public class CalculatorTests

    {

        private Calculator _calculator;
        [SetUp]

        public void BeforeEachTest()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_EmptyString_ShouldReturnZero()
        {
            // Arrange

            //Act
            int result = _calculator.Add("");

            //Assert

            Assert.That(result, Is.Zero);
        }

        [TestCase("1", 1)]
        [TestCase("84", 84)]
        public void Add_SingleNumber_ShouldReturnGivenNumber(string input, int expected)
        {

            int result = _calculator.Add(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1,2", 3)]
        [TestCase("84, 3", 87)]
        [TestCase("3, 2", 5)]
        public void Add_TwoNumbers_ShouldReturnSumOfNumbers(string input, int expected)
        {

            int result = _calculator.Add(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1,3,4", 8)]
        [TestCase("1,1,2,3,4", 11)]
        public void Add_MultipleNumbers_ShouldReturnSumOfNumbers(string input, int expected)
        {
            int result = _calculator.Add(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("1, -", "-")]
        public void InputString_Contains_InvalidCharacter_ShouldReturnException(string invalidInput, string invalidNumber)
        {
            Assert.That(() => _calculator.Add(invalidInput), Throws.ArgumentException.With.Message.Contains("Invalid number")
                .And.Message.Contains(invalidNumber));
        }

        [TestCase("1\n2,3", 6)]
        public void Add_ShouldUseNewLineAndCommaAsDelimiter(string input, int expected)
        {
            int result = _calculator.Add(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1,\n2")]
        public void InputString_Contains_InvalidDelimiterFormat_ShouldReturnException(string invalidInput)
        {
            Assert.That(() => _calculator.Add(invalidInput), Throws.ArgumentException.With.Message.Contains("Invalid inputformat"));
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("///\n2/3", 5)]
        [TestCase("//$\n2$8", 10)]
        public void Add_ShouldUseDelimiterSpecifiedInSeperateLine(string input, int expected)
        {
            int result = _calculator.Add(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("-1", "-1")]
        [TestCase("-2, 3, 2", "-2")]
        [TestCase("-1, -3, -5, 6, 1, -8", "-1, -3, -5, -8")]
        public void InputString_Contains_NegativeNumbers_ShouldThrowArgumentException(string input, string negativeNumbers)
        {
            Assert.That(() => _calculator.Add(input), Throws.ArgumentException.With.Message.Contains("Negatives not allowed").And.Message.Contains(negativeNumbers));
        }
    }
}