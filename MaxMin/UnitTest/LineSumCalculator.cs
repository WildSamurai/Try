namespace LineSumCalculator.Tests;

using FileLineProcessor;

using Xunit;

public class FileLineProcessorTests : IDisposable
{
    private readonly string _testDirectory;

    public FileLineProcessorTests()
    {
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

    private string CreateEmptyFile(string fileName)
    {
        string filePath = Path.Combine(_testDirectory, fileName);
        File.WriteAllText(filePath, string.Empty);
        return filePath;
    }

    [Fact]
    public void Constructor_EmptyFile_ReturnsIsEmptyTrue()
    {
        string filePath = CreateEmptyFile("empty.txt");

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);

        Assert.True(processor.IsEmptyFile());
        Assert.Null(processor.GetTargetLine());
        Assert.Empty(processor.GetWrongLines());
        Assert.Empty(processor.GetValidLines());
        Assert.Equal("File is empty", processor.GetTargetLineInfo());
        Assert.Equal("File is empty - no lines to check", processor.GetWrongLinesInfo());
    }

    [Fact]
    public void Constructor_ValidFileAndMaxStrategy_ProcessesCorrectly()
    {
        string[] lines = { "1,2,3", "4,5,6", "10,20,30" };
        string filePath = CreateTestFile("test1.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);

        Assert.False(processor.IsEmptyFile());
        Assert.Equal(3, processor.GetTargetLine().Value.lineNumber);
        Assert.Equal(60, processor.GetTargetLine().Value.sum);
        Assert.Empty(processor.GetWrongLines());
        Assert.Equal(3, processor.GetValidLines().Count);
    }

    [Fact]
    public void Constructor_FileWithEmptyLines_MarksEmptyLinesAsWrong()
    {
        string[] lines = { "1,2,3", "", "4,5,6", "   " };
        string filePath = CreateTestFile("empty_lines.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);

        Assert.False(processor.IsEmptyFile());
        Assert.Equal(3, processor.GetTargetLine().Value.lineNumber);
        Assert.Equal(15, processor.GetTargetLine().Value.sum);
        Assert.Equal(new[] { 2, 4 }, processor.GetWrongLines());
    }

    [Fact]
    public void Constructor_FileWithOnlyEmptyLines_ReturnsNoValidLines()
    {
        string[] lines = { "", "   ", "\t", " " };
        string filePath = CreateTestFile("only_empty.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);

        Assert.False(processor.IsEmptyFile());
        Assert.Null(processor.GetTargetLine());
        Assert.Equal(new[] { 1, 2, 3, 4 }, processor.GetWrongLines());
        Assert.Empty(processor.GetValidLines());
        Assert.Equal("No valid lines found", processor.GetTargetLineInfo());
    }

    [Fact]
    public void Constructor_ValidFileAndMinStrategy_ProcessesCorrectly()
    {
        string[] lines = { "10,20,30", "4,5,6", "1,2,3" };
        string filePath = CreateTestFile("test2.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MinSum);

        Assert.False(processor.IsEmptyFile());
        Assert.Equal(3, processor.GetTargetLine().Value.lineNumber);
        Assert.Equal(6, processor.GetTargetLine().Value.sum);
        Assert.Empty(processor.GetWrongLines());
    }

    [Fact]
    public void Constructor_FileWithErrors_IdentifiesWrongLines()
    {
        string[] lines = { "1,2,3", "4,abc,6", "7,8,9", "10,11x,12" };
        string filePath = CreateTestFile("test3.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);

        Assert.False(processor.IsEmptyFile());
        Assert.Equal(3, processor.GetTargetLine().Value.lineNumber);
        Assert.Equal(24, processor.GetTargetLine().Value.sum);
        Assert.Equal(new[] { 2, 4 }, processor.GetWrongLines());
        Assert.Equal(2, processor.GetValidLines().Count);
    }

    [Fact]
    public void Constructor_AllInvalidLines_ReturnsNullTargetLine()
    {
        string[] lines = { "1,abc,3", "x,y,z" };
        string filePath = CreateTestFile("test4.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);

        Assert.False(processor.IsEmptyFile());
        Assert.Null(processor.GetTargetLine());
        Assert.Equal("No valid lines found", processor.GetTargetLineInfo());
        Assert.Equal(new[] { 1, 2 }, processor.GetWrongLines());
    }

    [Fact]
    public void Constructor_FileNotFound_ThrowsFileNotFoundException()
    {
        string nonExistentFile = Path.Combine(_testDirectory, "nonexistent.txt");

        Assert.Throws<FileNotFoundException>(() =>
            new FileLineProcessor(nonExistentFile, CalculationStrategy.MaxSum));
    }

    [Fact]
    public void Constructor_EmptyFilePath_ThrowsArgumentException()
    {
        string emptyFilePath = "";

        Assert.Throws<ArgumentException>(() =>
            new FileLineProcessor(emptyFilePath, CalculationStrategy.MaxSum));
    }

    [Fact]
    public void GetTargetLineInfo_MaxStrategy_ReturnsCorrectInfo()
    {
        string[] lines = { "1,2,3", "10,20,30" };
        string filePath = CreateTestFile("test5.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);

        string info = processor.GetTargetLineInfo();

        Assert.Contains("Line with maximum sum: 2", info);
        Assert.Contains("Sum: 60.00", info);
    }

    [Fact]
    public void GetTargetLineInfo_MinStrategy_ReturnsCorrectInfo()
    {
        string[] lines = { "10,20,30", "1,2,3" };
        string filePath = CreateTestFile("test6.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MinSum);

        string info = processor.GetTargetLineInfo();

        Assert.Contains("Line with minimum sum: 2", info);
        Assert.Contains("Sum: 6.00", info);
    }

    [Fact]
    public void GetWrongLinesInfo_NoWrongLines_ReturnsAppropriateMessage()
    {
        string[] lines = { "1,2,3", "4,5,6" };
        string filePath = CreateTestFile("test7.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);

        string info = processor.GetWrongLinesInfo();

        Assert.Equal("No lines with wrong elements", info);
    }

    [Fact]
    public void MultipleMethodCalls_DoNotReprocessFile()
    {
        string[] lines = { "1,2,3", "4,5,6", "7,8,9" };
        string filePath = CreateTestFile("test8.txt", lines);

        var processor = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);

        var target1 = processor.GetTargetLine();
        var wrong1 = processor.GetWrongLines();
        var valid1 = processor.GetValidLines();

        var target2 = processor.GetTargetLine();
        var wrong2 = processor.GetWrongLines();
        var valid2 = processor.GetValidLines();

        Assert.Equal(target1.Value.lineNumber, target2.Value.lineNumber);
        Assert.Equal(target1.Value.sum, target2.Value.sum);
        Assert.Equal(wrong1, wrong2);
        Assert.Equal(valid1.Count, valid2.Count);
    }

    [Fact]
    public void LargeFile_ProcessesCorrectly()
    {
        var random = new Random();
        var lines = new string[1000];
        for (int i = 0; i < 1000; i++)
        {
            lines[i] = $"{random.Next(1, 100)},{random.Next(1, 100)},{random.Next(1, 100)}";
        }
        lines[500] = "1,1,1";
        lines[700] = "100,100,100";

        string filePath = CreateTestFile("large_test.txt", lines);

        var processorMin = new FileLineProcessor(filePath, CalculationStrategy.MinSum);
        Assert.Equal(501, processorMin.GetTargetLine().Value.lineNumber);
        Assert.Equal(3, processorMin.GetTargetLine().Value.sum);

        var processorMax = new FileLineProcessor(filePath, CalculationStrategy.MaxSum);
        Assert.Equal(701, processorMax.GetTargetLine().Value.lineNumber);
        Assert.Equal(300, processorMax.GetTargetLine().Value.sum);
    }
}