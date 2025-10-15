namespace Calculatorr;

public class Calculator
{
    private readonly Dictionary<char, int> _operatorsPriority = new Dictionary<char, int>
    {
        { '+', 1 },
        { '-', 1 },
        { '*', 2 },
        { '/', 2 },
        { 'u', 3 },
        { '(', 0 },
        { ')', 0 }
    };

    public double Evaluate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentException("Expression cannot be empty");

        var tokens = Tokenize(expression);
        ValidateTokens(tokens);
        var postfix = ConvertToPostfix(tokens);
        return EvaluatePostfix(postfix);
    }

    private void ValidateTokens(List<string> tokens)
    {
        for (int i = 0; i < tokens.Count - 1; i++)
        {
            if (tokens[i] == "(" && tokens[i + 1] == ")")
            {
                throw new ArgumentException("Empty parentheses are not allowed");
            }
        }

        if (tokens.Count == 0)
            throw new ArgumentException("Expression contains no valid tokens");

        string lastToken = tokens.Last();
        if (lastToken == "+" || lastToken == "-" || lastToken == "*" || lastToken == "/")
        {
            throw new ArgumentException("Expression cannot end with an operator");
        }

        string firstToken = tokens.First();
        if (firstToken == "+" || firstToken == "*" || firstToken == "/")
        {
            throw new ArgumentException("Expression cannot start with this operator");
        }
    }

    private List<string> Tokenize(string expression)
    {
        var tokens = new List<string>();
        int i = 0;

        while (i < expression.Length)
        {
            if (char.IsWhiteSpace(expression[i]))
            {
                i++;
                continue;
            }

            if (expression[i] == '-' && (i == 0 || tokens.Count == 0 ||
                tokens.Last() == "(" || IsOperator(tokens.Last())))
            {
                tokens.Add("u");
                i++;
            }
            else if (char.IsDigit(expression[i]) || expression[i] == '.')
            {
                string number = "";
                while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                {
                    number += expression[i];
                    i++;
                }
                tokens.Add(number);
            }
            else if (_operatorsPriority.ContainsKey(expression[i]) || expression[i] == '(' || expression[i] == ')')
            {
                tokens.Add(expression[i].ToString());
                i++;
            }
            else
            {
                throw new ArgumentException($"Invalid character: '{expression[i]}'");
            }
        }

        return tokens;
    }

    private bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "u";
    }

    private List<string> ConvertToPostfix(List<string> tokens)
    {
        var output = new List<string>();
        var operators = new Stack<string>();

        foreach (var token in tokens)
        {
            if (IsNumber(token))
            {
                output.Add(token);
            }
            else if (token == "(")
            {
                operators.Push(token);
            }
            else if (token == ")")
            {
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    output.Add(operators.Pop());
                }

                if (operators.Count == 0)
                    throw new ArgumentException("Mismatched parentheses - missing opening parenthesis");

                operators.Pop();
            }
            else if (IsOperator(token))
            {
                int currentPriority = _operatorsPriority[token[0]];
                while (operators.Count > 0 && operators.Peek() != "(" &&
                       _operatorsPriority[operators.Peek()[0]] >= currentPriority)
                {
                    output.Add(operators.Pop());
                }
                operators.Push(token);
            }
        }

        while (operators.Count > 0)
        {
            if (operators.Peek() == "(")
                throw new ArgumentException("Mismatched parentheses - missing closing parenthesis");

            output.Add(operators.Pop());
        }

        return output;
    }

    private double EvaluatePostfix(List<string> postfix)
    {
        var stack = new Stack<double>();

        foreach (var token in postfix)
        {
            if (IsNumber(token))
            {
                if (!double.TryParse(token, out double number))
                    throw new ArgumentException($"Invalid number: '{token}'");

                stack.Push(number);
            }
            else if (token == "u")
            {
                if (stack.Count < 1)
                    throw new ArgumentException("Invalid expression for unary minus");

                double operand = stack.Pop();
                stack.Push(-operand);
            }
            else
            {
                if (stack.Count < 2)
                    throw new ArgumentException("Invalid expression - not enough operands");

                double right = stack.Pop();
                double left = stack.Pop();
                double result = PerformOperation(left, right, token[0]);
                stack.Push(result);
            }
        }

        if (stack.Count != 1)
            throw new ArgumentException("Invalid expression - too many operands");

        return stack.Pop();
    }

    private double PerformOperation(double left, double right, char operation)
    {
        switch (operation)
        {
            case '+':
                return left + right;
            case '-':
                return left - right;
            case '*':
                return left * right;
            case '/':
                if (right == 0)
                    throw new DivideByZeroException("Division by zero is not allowed");
                return left / right;
            default:
                throw new ArgumentException($"Unknown operation: '{operation}'");
        }
    }

    private bool IsNumber(string token)
    {
        if (string.IsNullOrEmpty(token))
            return false;

        if (token[0] == '-' && token.Length > 1)
        {
            return double.TryParse(token, out _);
        }

        return double.TryParse(token, out _);
    }
}