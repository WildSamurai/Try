namespace BookLibrary.Tests.Services;

using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Services;
using BookLibrary.Entities;
using BookLibrary.Models;
using BookLibrary.Tests.TestHelpers;
using Xunit;

public class BookServiceTests : IAsyncLifetime
{
    private ApplicationDbContext _context;
    private BookService _bookService;

    public async Task InitializeAsync()
    {
        _context = await TestDbContextFactory.CreateInMemoryDbContextWithData();
        _bookService = new BookService(_context);
    }

    public async Task DisposeAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.DisposeAsync();
    }

    [Fact]
    public async Task AddBooksFromFileAsync_ValidFile_AddsBooksToDatabase()
    {
        var testFile = "TestData/test_books.csv";
        var initialBookCount = await _context.Books.CountAsync();

        var result = await _bookService.AddBooksFromFileAsync(testFile);

        Assert.True(result);
        var finalBookCount = await _context.Books.CountAsync();
        Assert.True(finalBookCount > initialBookCount);

        var addedBook = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.Title == "Harry Potter and the Philosopher's Stone");

        Assert.NotNull(addedBook);
        Assert.Equal("J.K. Rowling", addedBook.Author.Name);
        Assert.Equal("Fantasy", addedBook.Genre.Name);
    }

    [Fact]
    public async Task AddBooksFromFileAsync_FileNotFound_ThrowsFileNotFoundException()
    {
        var nonExistentFile = "nonexistent.csv";

        await Assert.ThrowsAsync<FileNotFoundException>(() =>
            _bookService.AddBooksFromFileAsync(nonExistentFile));
    }

    [Fact]
    public async Task AddBooksFromFileAsync_InvalidData_SkipsInvalidLines()
    {
        var invalidFile = "TestData/test_books_invalid.csv";
        var initialBookCount = await _context.Books.CountAsync();

        var result = await _bookService.AddBooksFromFileAsync(invalidFile);

        Assert.True(result);
        var finalBookCount = await _context.Books.CountAsync();
        Assert.Equal(initialBookCount + 1, finalBookCount);
    }

    [Fact]
    public async Task AddBooksFromFileAsync_DuplicateBooks_DoesNotAddDuplicates()
    {
        var testFile = "TestData/test_books.csv";
        await _bookService.AddBooksFromFileAsync(testFile);
        var countAfterFirstImport = await _context.Books.CountAsync();

        var result = await _bookService.AddBooksFromFileAsync(testFile);

        Assert.True(result);
        var countAfterSecondImport = await _context.Books.CountAsync();
        Assert.Equal(countAfterFirstImport, countAfterSecondImport);
    }

    [Fact]
    public async Task SearchBooksAsync_WithTitleFilter_ReturnsMatchingBooks()
    {
        var filter = new Filter { Title = "Hobbit" };

        var (count, books) = await _bookService.SearchBooksAsync(filter);

        Assert.Equal(1, count);
        Assert.Single(books);
        Assert.Contains("Hobbit", books.First().Title);
    }

    [Fact]
    public async Task SearchBooksAsync_WithAuthorFilter_ReturnsMatchingBooks()
    {
        var filter = new Filter { Author = "Tolkien" };

        var (count, books) = await _bookService.SearchBooksAsync(filter);

        Assert.Equal(2, count);
        Assert.All(books, book =>
            Assert.Contains("Tolkien", book.Author.Name));
    }

    [Fact]
    public async Task SearchBooksAsync_WithGenreFilter_ReturnsMatchingBooks()
    {
        var filter = new Filter { Genre = "Fantasy" };

        var (count, books) = await _bookService.SearchBooksAsync(filter);

        Assert.Equal(2, count);
        Assert.All(books, book =>
            Assert.Equal("Fantasy", book.Genre.Name));
    }

    [Fact]
    public async Task SearchBooksAsync_WithPageCountFilter_ReturnsMatchingBooks()
    {
        var filter = new Filter { MoreThanPages = 300 };

        var (count, books) = await _bookService.SearchBooksAsync(filter);

        Assert.Equal(1, count);
        Assert.All(books, book => Assert.True(book.Pages > 300));
    }

    [Fact]
    public async Task SearchBooksAsync_WithDateFilter_ReturnsMatchingBooks()
    {
        var filter = new Filter { PublishedAfter = new DateTime(1950, 1, 1) };

        var (count, books) = await _bookService.SearchBooksAsync(filter);

        Assert.Equal(1, count);
        Assert.All(books, book => Assert.True(book.ReleaseDate > new DateTime(1950, 1, 1)));
    }

    [Fact]
    public async Task SearchBooksAsync_WithMultipleFilters_ReturnsMatchingBooks()
    {
        var filter = new Filter
        {
            Genre = "Fantasy",
            MoreThanPages = 100,
            LessThanPages = 400
        };

        var (count, books) = await _bookService.SearchBooksAsync(filter);

        Assert.Equal(1, count);
        var book = books.First();
        Assert.Equal("Fantasy", book.Genre.Name);
        Assert.True(book.Pages > 100 && book.Pages < 400);
    }

    [Fact]
    public async Task SearchBooksAsync_NoFilters_ReturnsAllBooks()
    {
        var filter = new Filter();
        var totalBooks = await _context.Books.CountAsync();

        var (count, books) = await _bookService.SearchBooksAsync(filter);

        Assert.Equal(totalBooks, count);
        Assert.Equal(totalBooks, books.Count);
    }

    [Fact]
    public async Task SaveBooksToFileAsync_ValidBooks_CreatesFileWithCorrectContent()
    {
        var testFile = "test_export.csv";
        var (_, books) = await _bookService.SearchBooksAsync(new Filter());

        var result = await _bookService.SaveBooksToFileAsync(books, testFile);

        Assert.True(result);
        Assert.True(File.Exists(testFile));

        var lines = await File.ReadAllLinesAsync(testFile);
        Assert.NotEmpty(lines);
        Assert.Equal("Title,Pages,Genre,Author,Publisher,ReleaseDate", lines[0]);

        var content = await File.ReadAllTextAsync(testFile);
        Assert.Contains("The Hobbit", content);
        Assert.Contains("Fantasy", content);

        File.Delete(testFile);
    }

    [Fact]
    public async Task SaveBooksToFileAsync_EmptyList_CreatesFileWithOnlyHeader()
    {
        var testFile = "test_empty_export.csv";
        var emptyBookList = new List<Book>();

        var result = await _bookService.SaveBooksToFileAsync(emptyBookList, testFile);

        Assert.True(result);
        Assert.True(File.Exists(testFile));

        var lines = await File.ReadAllLinesAsync(testFile);
        Assert.Single(lines);
        Assert.Equal("Title,Pages,Genre,Author,Publisher,ReleaseDate", lines[0]);

        File.Delete(testFile);
    }

    [Fact]
    public async Task GetTotalBookCountAsync_ReturnsCorrectCount()
    {
        var expectedCount = await _context.Books.CountAsync();

        var actualCount = await _bookService.GetTotalBookCountAsync();

        Assert.Equal(expectedCount, actualCount);
    }
}