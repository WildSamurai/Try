namespace FileProcessor;

using Calculatorr;
public class FileProcessor
{
    private readonly Calculator _calculator;

    public FileProcessor()
    {
        _calculator = new Calculator();
    }

    public void ProcessFile(string inputFilePath, string outputFilePath)
    {
        if (!File.Exists(inputFilePath))
            throw new FileNotFoundException($"Input file not found: {inputFilePath}");

        var inputLines = File.ReadAllLines(inputFilePath);
        var outputLines = new List<string>();

        foreach (var line in inputLines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                outputLines.Add("");
                continue;
            }

            string trimmedLine = line.Trim();
            try
            {
                double result = _calculator.Evaluate(trimmedLine);
                outputLines.Add($"{trimmedLine} = {result}");
            }
            catch (Exception ex)
            {
                outputLines.Add($"{trimmedLine} = Error: {ex.Message}");
            }
        }

        File.WriteAllLines(outputFilePath, outputLines);
    }
}