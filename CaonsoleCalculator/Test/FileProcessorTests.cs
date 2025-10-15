namespace Test;
using FileProcessor;
public class FileProcessorTests : IDisposable
{
    private readonly FileProcessor _fileProcessor;
    private readonly string _testDirectory;

    public FileProcessorTests()
    {
        _fileProcessor = new FileProcessor();
        _testDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(_testDirectory);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }

    private string CreateTestFile(string fileName, string[] lines)
    {
        string filePath = Path.Combine(_testDirectory, fileName);
        File.WriteAllLines(filePath, lines);
        return filePath;
    }

    [Fact]
    public void ProcessFile_ValidExpressions_WritesCorrectResults()
    {
        string[] inputLines = {
            "2+2",
            "3*4",
            "10-5",
            "15/3",
            "1+2*(3+2)"
        };
        string inputFile = CreateTestFile("input.txt", inputLines);
        string outputFile = Path.Combine(_testDirectory, "output.txt");

        _fileProcessor.ProcessFile(inputFile, outputFile);

        string[] outputLines = File.ReadAllLines(outputFile);
        Assert.Equal(5, outputLines.Length);
        Assert.Equal("2+2 = 4", outputLines[0]);
        Assert.Equal("3*4 = 12", outputLines[1]);
        Assert.Equal("10-5 = 5", outputLines[2]);
        Assert.Equal("15/3 = 5", outputLines[3]);
        Assert.Equal("1+2*(3+2) = 11", outputLines[4]);
    }

    [Fact]
    public void ProcessFile_InvalidExpressions_WritesErrorMessages()
    {
        string[] inputLines = {
            "2+2",
            "1+x+4",
            "10/0",
            "2*3",
            "invalid"
        };
        string inputFile = CreateTestFile("input.txt", inputLines);
        string outputFile = Path.Combine(_testDirectory, "output.txt");

        _fileProcessor.ProcessFile(inputFile, outputFile);

        string[] outputLines = File.ReadAllLines(outputFile);
        Assert.Equal(5, outputLines.Length);
        Assert.Equal("2+2 = 4", outputLines[0]);
        Assert.Contains("Error", outputLines[1]);
        Assert.Contains("Division by zero", outputLines[2]);
        Assert.Equal("2*3 = 6", outputLines[3]);
        Assert.Contains("Error", outputLines[4]);
    }

    [Fact]
    public void ProcessFile_EmptyLines_HandlesCorrectly()
    {
        string[] inputLines = {
            "2+2",
            "",
            "3*4",
            "   ",
            "10-5"
        };
        string inputFile = CreateTestFile("input.txt", inputLines);
        string outputFile = Path.Combine(_testDirectory, "output.txt");

        _fileProcessor.ProcessFile(inputFile, outputFile);

        string[] outputLines = File.ReadAllLines(outputFile);
        Assert.Equal(5, outputLines.Length);
        Assert.Equal("2+2 = 4", outputLines[0]);
        Assert.Equal("", outputLines[1]);
        Assert.Equal("3*4 = 12", outputLines[2]);
        Assert.Equal("   = Error", outputLines[3]);
        Assert.Equal("10-5 = 5", outputLines[4]);
    }

    [Fact]
    public void ProcessFile_ComplexExpressions_WritesCorrectResults()
    {
        string[] inputLines = {
            "2+15/3+4*2",
            "(2+3)*(4+5)",
            "10.5/2+3.5*2"
        };
        string inputFile = CreateTestFile("input.txt", inputLines);
        string outputFile = Path.Combine(_testDirectory, "output.txt");

        _fileProcessor.ProcessFile(inputFile, outputFile);

        string[] outputLines = File.ReadAllLines(outputFile);
        Assert.Equal(3, outputLines.Length);
        Assert.Equal("2+15/3+4*2 = 15", outputLines[0]);
        Assert.Equal("(2+3)*(4+5) = 45", outputLines[1]);
        Assert.Equal("10.5/2+3.5*2 = 11.25", outputLines[2]);
    }

    [Fact]
    public void ProcessFile_InputFileNotFound_ThrowsFileNotFoundException()
    {
        string nonExistentFile = Path.Combine(_testDirectory, "nonexistent.txt");
        string outputFile = Path.Combine(_testDirectory, "output.txt");

        Assert.Throws<FileNotFoundException>(() => _fileProcessor.ProcessFile(nonExistentFile, outputFile));
    }

    [Fact]
    public void ProcessFile_EmptyFile_CreatesEmptyOutputFile()
    {
        string[] inputLines = Array.Empty<string>();
        string inputFile = CreateTestFile("input.txt", inputLines);
        string outputFile = Path.Combine(_testDirectory, "output.txt");

        _fileProcessor.ProcessFile(inputFile, outputFile);

        string[] outputLines = File.ReadAllLines(outputFile);
        Assert.Empty(outputLines);
    }
}
