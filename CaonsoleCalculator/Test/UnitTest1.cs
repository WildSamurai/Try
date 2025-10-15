namespace Tests;
using System;

using Calculatorr;

using Xunit;

public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        _calculator = new Calculator();
    }

    [Theory]
    [InlineData("2+2", 4)]
    [InlineData("10-5", 5)]
    [InlineData("6*7", 42)]
    [InlineData("15/3", 5)]
    [InlineData("2.5+3.5", 6)]
    [InlineData("10.5/2", 5.25)]
    public void Evaluate_SimpleOperations_ReturnsCorrectResult(string expression, double expected)
    {
        double result = _calculator.Evaluate(expression);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2+2*3", 8)]
    [InlineData("2+3*4-1", 13)]
    [InlineData("10-3*2", 4)]
    [InlineData("8/2*4", 16)]
    [InlineData("2*3+4*5", 26)]
    public void Evaluate_OperatorPrecedence_Respected(string expression, double expected)
    {
        double result = _calculator.Evaluate(expression);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1+2*(3+2)", 11)]
    [InlineData("(2+3)*4", 20)]
    [InlineData("2*(3+4)*2", 28)]
    [InlineData("((2+3)*4)/2", 10)]
    [InlineData("(1+2)*(3+4)", 21)]
    [InlineData("(5*(3+2))/(2+3)", 5)]
    public void Evaluate_ExpressionsWithBrackets_ReturnsCorrectResult(string expression, double expected)
    {
        double result = _calculator.Evaluate(expression);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2 + 3", 5)]
    [InlineData("  10 - 5  ", 5)]
    [InlineData("6 * 7 ", 42)]
    [InlineData(" 15 / 3 ", 5)]
    public void Evaluate_ExpressionsWithSpaces_ReturnsCorrectResult(string expression, double expected)
    {
        double result = _calculator.Evaluate(expression);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Evaluate_DivisionByZero_ThrowsDivideByZeroException()
    {
        string expression = "2/0";

        var exception = Assert.Throws<DivideByZeroException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Division by zero", exception.Message);
    }

    [Theory]
    [InlineData("1+x+4")]
    [InlineData("2+abc")]
    [InlineData("12.34.56")]
    [InlineData("2++3")]
    [InlineData("2*/3")]
    [InlineData("(2+3")]
    [InlineData("2+3)")]
    [InlineData("((2+3)")]
    [InlineData("2+3))")]
    public void Evaluate_InvalidExpressions_ThrowsArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Evaluate_EmptyOrNullExpression_ThrowsArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("0.1+0.2", 0.3)]
    [InlineData("3.14*2", 6.28)]
    [InlineData("10.75/2.5", 4.3)]
    [InlineData("0.5*0.5", 0.25)]
    public void Evaluate_DecimalNumbers_ReturnsCorrectResult(string expression, double expected)
    {
        double result = _calculator.Evaluate(expression);

        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2", 2)]
    [InlineData("3.14", 3.14)]
    [InlineData("0", 0)]
    public void Evaluate_SingleNumber_ReturnsNumber(string expression, double expected)
    {
        double result = _calculator.Evaluate(expression);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("10-5-2", 3)]
    [InlineData("20/5/2", 2)]
    [InlineData("2*3*4", 24)]
    [InlineData("1+2+3+4", 10)]
    public void Evaluate_MultipleSamePrecedenceOperations_LeftToRight(string expression, double expected)
    {
        double result = _calculator.Evaluate(expression);

        Assert.Equal(expected, result);
    }
}