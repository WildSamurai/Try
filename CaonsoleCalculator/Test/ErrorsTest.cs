namespace ErrorsTest;

using System;

using Calculatorr;

using Xunit;
public class CalculatorErrorHandlingTests
{
    private readonly Calculator _calculator;

    public CalculatorErrorHandlingTests()
    {
        _calculator = new Calculator();
    }

    [Theory]
    [InlineData("()")]
    [InlineData("()-(3+3)")]
    [InlineData("2+()")]
    [InlineData("()*5")]
    [InlineData("( )")]
    [InlineData("( ) + 2")]
    public void Evaluate_EmptyParentheses_ThrowsArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Empty parentheses", exception.Message);
    }

    [Theory]
    [InlineData("2+")]
    [InlineData("3-")]
    [InlineData("4*")]
    [InlineData("5/")]
    [InlineData("+")]
    [InlineData("*")]
    [InlineData("/")]
    public void Evaluate_ExpressionEndsWithOperator_ThrowsArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("end with an operator", exception.Message);
    }

    [Theory]
    [InlineData("+2")]
    [InlineData("*3")]
    [InlineData("/4")]
    public void Evaluate_ExpressionStartsWithInvalidOperator_ThrowsArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("start with this operator", exception.Message);
    }

    [Theory]
    [InlineData("(2+3")]
    [InlineData("2+3)")]
    [InlineData("((2+3)")]
    [InlineData("(2+3))")]
    [InlineData(")2+3(")]
    public void Evaluate_MismatchedParentheses_ThrowsArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
    }

    [Theory]
    [InlineData("2++3")]
    [InlineData("2*/3")]
    [InlineData("2-+3")]
    [InlineData("2/*3")]
    public void Evaluate_ConsecutiveOperators_ThrowsArgumentException(string expression)
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
    [InlineData("2&3")]
    [InlineData("abc")]
    [InlineData("2a+3")]
    [InlineData("2+3x")]
    public void Evaluate_InvalidCharacters_ThrowsArgumentException(string expression)
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Evaluate(expression));
        Assert.Contains("Invalid character", exception.Message);
    }

    [Fact]
    public void Evaluate_ValidComplexExpressionWithUnaryMinus_ReturnsCorrectResult()
    {
        string expression = "-(2+3)*-(4+1)";

        double result = _calculator.Evaluate(expression);

        Assert.Equal(25, result);
    }

    [Fact]
    public void Evaluate_ValidExpressionWithSpaces_ReturnsCorrectResult()
    {
        string expression = " 2 + 3 * ( 4 - 1 ) ";

        double result = _calculator.Evaluate(expression);

        Assert.Equal(11, result);
    }
}