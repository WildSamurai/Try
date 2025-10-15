namespace CalculatorApp;

using Calculatorr;

using FileProcessor;
public class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            RunConsoleMode();
        }
        else if (args.Length == 2)
        {
            RunFileMode(args[0], args[1]);
        }
        else
        {
            ShowUsage();
        }
    }

    static void RunConsoleMode()
    {
        var calculator = new Calculator();

        Console.WriteLine("Calculator");
        Console.WriteLine("Supported operations: +, -, *, /, ()");
        Console.WriteLine("Examples: 2+2*3, 1+2*(3+2), (2+3)*4");
        Console.WriteLine();
        Console.Write("Enter expression: ");

        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("No expression provided.");
            return;
        }

        try
        {
            double result = calculator.Evaluate(input);
            Console.WriteLine($"Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void RunFileMode(string inputFile, string outputFile)
    {
        try
        {
            var processor = new FileProcessor();
            processor.ProcessFile(inputFile, outputFile);
            Console.WriteLine($"Processing completed. Results saved to: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing file: {ex.Message}");
        }
    }

    static void ShowUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  Console mode (single expression): CalculatorApp.exe");
        Console.WriteLine("  File mode: CalculatorApp.exe <input_file> <output_file>");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  CalculatorApp.exe");
        Console.WriteLine("  CalculatorApp.exe input.txt output.txt");
    }
}