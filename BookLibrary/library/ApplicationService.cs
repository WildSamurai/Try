namespace BookLibrary.Services;

using BookLibrary.Models;

public class ApplicationService
{
    private readonly IBookService _bookService;
    private readonly IFilterService _filterService;

    public ApplicationService(IBookService bookService, IFilterService filterService)
    {
        _bookService = bookService;
        _filterService = filterService;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Book Library Management ===");
            Console.WriteLine("1. Add Books from CSV File");
            Console.WriteLine("2. Search Books");
            Console.WriteLine("3. Exit");
            Console.Write("Choose option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await AddBooksFromFileAsync();
                    break;
                case "2":
                    await SearchBooksAsync();
                    break;
                case "3":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task AddBooksFromFileAsync()
    {
        Console.Write("Enter CSV file path (or press Enter for 'books.csv'): ");
        var filePath = Console.ReadLine()?.Trim() ?? "books.csv";

        try
        {
            var success = await _bookService.AddBooksFromFileAsync(filePath);
            if (success)
            {
                var count = await _bookService.GetTotalBookCountAsync();
                Console.WriteLine($"Books added successfully! Total books in database: {count}");
            }
            else
            {
                Console.WriteLine("No books were added (possible duplicates or errors).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task SearchBooksAsync()
    {
        try
        {
            var filter = await _filterService.LoadFilterAsync();
            if (filter == null)
            {
                Console.WriteLine("filter.json not found. Using empty filter.");
                filter = new Filter();
            }

            var (count, books) = await _bookService.SearchBooksAsync(filter);

            Console.WriteLine($"\nFound {count} books matching the criteria:");
            foreach (var book in books)
            {
                Console.WriteLine($"- {book.Title} ({book.ReleaseDate:yyyy})");
            }

            if (books.Any())
            {
                var outputFileName = $"search_results_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                var success = await _bookService.SaveBooksToFileAsync(books, outputFileName);
                if (success)
                    Console.WriteLine($"\nResults saved to: {outputFileName}");
                else
                    Console.WriteLine("\nFailed to save results to file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}