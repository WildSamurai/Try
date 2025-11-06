namespace BookLibrary.Tests.Services;

using BookLibrary.Services;
using BookLibrary.Models;
using Xunit;

public class FilterServiceTests
{
    private readonly FilterService _filterService;

    public FilterServiceTests()
    {
        _filterService = new FilterService();
    }

    [Fact]
    public async Task LoadFilterAsync_ValidFile_ReturnsFilterObject()
    {
        var testFile = "TestData/test_filter.json";

        var result = await _filterService.LoadFilterAsync(testFile);

        Assert.NotNull(result);
        Assert.Equal("Harry Potter", result.Title);
        Assert.Equal("Fantasy", result.Genre);
        Assert.Equal(300, result.MoreThanPages);
        Assert.Equal(new DateTime(1990, 1, 1), result.PublishedAfter);
    }

    [Fact]
    public async Task LoadFilterAsync_FileNotFound_ReturnsNull()
    {
        var result = await _filterService.LoadFilterAsync("nonexistent.json");

        Assert.Null(result);
    }

    [Fact]
    public async Task LoadFilterAsync_InvalidJson_ReturnsNull()
    {
        var invalidFile = "invalid.json";
        await File.WriteAllTextAsync(invalidFile, "invalid json content");

        var result = await _filterService.LoadFilterAsync(invalidFile);

        Assert.Null(result);

        File.Delete(invalidFile);
    }

    [Fact]
    public async Task SaveFilterAsync_ValidFilter_CreatesFileWithCorrectContent()
    {
        var testFile = "test_save_filter.json";
        var filter = new Filter
        {
            Title = "Test Book",
            Genre = "Fiction",
            Author = "Test Author",
            Publisher = "Test Publisher",
            MoreThanPages = 200,
            LessThanPages = 500,
            PublishedAfter = new DateTime(2000, 1, 1),
            PublishedBefore = new DateTime(2020, 12, 31)
        };

        var result = await _filterService.SaveFilterAsync(filter, testFile);

        Assert.True(result);
        Assert.True(File.Exists(testFile));

        var content = await File.ReadAllTextAsync(testFile);
        Assert.Contains("Test Book", content);
        Assert.Contains("Fiction", content);
        Assert.Contains("Test Author", content);
        Assert.Contains("200", content);
        Assert.Contains("500", content);

        File.Delete(testFile);
    }

    [Fact]
    public async Task SaveFilterAsync_NullFilter_CreatesEmptyFilterFile()
    {
        var testFile = "test_empty_filter.json";
        var filter = new Filter();

        var result = await _filterService.SaveFilterAsync(filter, testFile);

        Assert.True(result);
        Assert.True(File.Exists(testFile));

        var content = await File.ReadAllTextAsync(testFile);
        Assert.DoesNotContain("title", content);
        Assert.Contains("title", content.ToLower());

        File.Delete(testFile);
    }

    [Fact]
    public async Task SaveAndLoadFilterAsync_RoundTrip_ReturnsSameFilter()
    {
        var testFile = "test_roundtrip.json";
        var originalFilter = new Filter
        {
            Title = "Roundtrip Test",
            Genre = "Sci-Fi",
            MoreThanPages = 100
        };

        var saveResult = await _filterService.SaveFilterAsync(originalFilter, testFile);
        var loadedFilter = await _filterService.LoadFilterAsync(testFile);

        Assert.True(saveResult);
        Assert.NotNull(loadedFilter);
        Assert.Equal(originalFilter.Title, loadedFilter.Title);
        Assert.Equal(originalFilter.Genre, loadedFilter.Genre);
        Assert.Equal(originalFilter.MoreThanPages, loadedFilter.MoreThanPages);

        File.Delete(testFile);
    }
}