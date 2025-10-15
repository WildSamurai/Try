namespace FileLineProcessor;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class FileLineProcessor
{
    private readonly string _filePath;
    private readonly CalculationStrategy _strategy;
    private readonly List<int> _wrongLines;
    private readonly List<(int lineNumber, double sum)> _validLines;
    private readonly (int lineNumber, double sum)? _targetLine;
    private readonly bool _isEmptyFile;

    public FileLineProcessor(string filePath, CalculationStrategy strategy)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

        _filePath = filePath;
        _strategy = strategy;
        _wrongLines = new List<int>();
        _validLines = new List<(int, double)>();

        _isEmptyFile = ProcessFile();
        _targetLine = CalculateTargetLine();
    }

    private bool ProcessFile()
    {
        if (!File.Exists(_filePath))
            throw new FileNotFoundException($"File not found: {_filePath}");

        var lines = File.ReadAllLines(_filePath);

        if (lines.Length == 0)
            return true;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            int lineNumber = i + 1;

            if (TryParseLine(line, out double sum))
            {
                _validLines.Add((lineNumber, sum));
            }
            else
            {
                _wrongLines.Add(lineNumber);
            }
        }

        return false;
    }

    private (int lineNumber, double sum)? CalculateTargetLine()
    {
        if (_isEmptyFile || !_validLines.Any())
            return null;

        return _strategy == CalculationStrategy.MaxSum
            ? _validLines.OrderByDescending(x => x.sum).First()
            : _validLines.OrderBy(x => x.sum).First();
    }

    private bool TryParseLine(string line, out double sum)
    {
        sum = 0;
        if (string.IsNullOrWhiteSpace(line))
            return false;

        string[] parts = line.Split(',');

        foreach (string part in parts)
        {
            string trimmedPart = part.Trim();
            if (double.TryParse(trimmedPart, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double number))
            {
                sum += number;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public IReadOnlyList<int> GetWrongLines() => _wrongLines.AsReadOnly();
    public IReadOnlyList<(int lineNumber, double sum)> GetValidLines() => _validLines.AsReadOnly();
    public (int lineNumber, double sum)? GetTargetLine() => _targetLine;
    public bool IsEmptyFile() => _isEmptyFile;

    public string GetTargetLineInfo()
    {
        if (_isEmptyFile)
            return "File is empty";

        if (_targetLine == null)
            return "No valid lines found";

        string mode = _strategy == CalculationStrategy.MaxSum ? "maximum" : "minimum";
        return $"Line with {mode} sum: {_targetLine.Value.lineNumber}, Sum: {_targetLine.Value.sum:F2}";
    }

    public string GetWrongLinesInfo()
    {
        if (_isEmptyFile)
            return "File is empty - no lines to check";

        if (!_wrongLines.Any())
            return "No lines with wrong elements";

        return $"Lines with wrong elements: {string.Join(", ", _wrongLines)}";
    }
}