namespace BookLibrary.Services;

using System.Text;

using BookLibrary.Data;
using BookLibrary.Entities;
using BookLibrary.Models;

using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddBooksFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var lines = await File.ReadAllLinesAsync(filePath);
        var books = new List<Book>();
        var genres = new Dictionary<string, Genre>();
        var authors = new Dictionary<string, Author>();
        var publishers = new Dictionary<string, Publisher>();

        int startIndex = lines[0].Contains("Title") ? 1 : 0;

        for (int i = startIndex; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = ParseCsvLine(line);
            if (parts.Length < 6) continue;

            try
            {
                var title = parts[0].Trim('"', ' ').Trim();
                if (string.IsNullOrEmpty(title)) continue;

                if (!int.TryParse(parts[1].Trim(), out int pages))
                    continue;

                var genreName = parts[2].Trim('"', ' ').Trim();
                var authorName = parts[3].Trim('"', ' ').Trim();
                var publisherName = parts[4].Trim('"', ' ').Trim();

                if (!DateTime.TryParse(parts[5].Trim(), out DateTime releaseDate))
                    continue;

                var existingBook = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .FirstOrDefaultAsync(b => b.Title == title &&
                                             b.Author.Name == authorName &&
                                             b.ReleaseDate.Year == releaseDate.Year);

                if (existingBook != null) continue;

                if (!genres.ContainsKey(genreName))
                {
                    var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreName)
                               ?? new Genre { Id = Guid.NewGuid(), Name = genreName };
                    genres[genreName] = genre;
                }

                if (!authors.ContainsKey(authorName))
                {
                    var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == authorName)
                                ?? new Author { Id = Guid.NewGuid(), Name = authorName };
                    authors[authorName] = author;
                }

                if (!publishers.ContainsKey(publisherName))
                {
                    var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == publisherName)
                                   ?? new Publisher { Id = Guid.NewGuid(), Name = publisherName };
                    publishers[publisherName] = publisher;
                }

                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Pages = pages,
                    GenreId = genres[genreName].Id,
                    AuthorId = authors[authorName].Id,
                    PublisherId = publishers[publisherName].Id,
                    ReleaseDate = releaseDate
                };

                books.Add(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing line {i + 1}: {ex.Message}");
            }
        }

        await _context.Genres.AddRangeAsync(genres.Values.Where(g => g.Id != default));
        await _context.Authors.AddRangeAsync(authors.Values.Where(a => a.Id != default));
        await _context.Publishers.AddRangeAsync(publishers.Values.Where(p => p.Id != default));
        await _context.Books.AddRangeAsync(books);

        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<(int count, List<Book> books)> SearchBooksAsync(Filter filter)
    {
        var query = _context.Books
            .Include(b => b.Genre)
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(b => b.Title.Contains(filter.Title));

        if (!string.IsNullOrEmpty(filter.Genre))
            query = query.Where(b => b.Genre.Name.Contains(filter.Genre));

        if (!string.IsNullOrEmpty(filter.Author))
            query = query.Where(b => b.Author.Name.Contains(filter.Author));

        if (!string.IsNullOrEmpty(filter.Publisher))
            query = query.Where(b => b.Publisher.Name.Contains(filter.Publisher));

        if (filter.MoreThanPages.HasValue)
            query = query.Where(b => b.Pages > filter.MoreThanPages.Value);

        if (filter.LessThanPages.HasValue)
            query = query.Where(b => b.Pages < filter.LessThanPages.Value);

        if (filter.PublishedBefore.HasValue)
            query = query.Where(b => b.ReleaseDate < filter.PublishedBefore.Value);

        if (filter.PublishedAfter.HasValue)
            query = query.Where(b => b.ReleaseDate > filter.PublishedAfter.Value);

        var books = await query.ToListAsync();
        return (books.Count, books);
    }

    public async Task<bool> SaveBooksToFileAsync(List<Book> books, string filePath)
    {
        try
        {
            var lines = new List<string> { "Title,Pages,Genre,Author,Publisher,ReleaseDate" };
            lines.AddRange(books.Select(b =>
                $"{b.Title},{b.Pages},{b.Genre.Name},{b.Author.Name},{b.Publisher.Name},{b.ReleaseDate:yyyy-MM-dd}"));

            await File.WriteAllLinesAsync(filePath, lines);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<int> GetTotalBookCountAsync()
    {
        return await _context.Books.CountAsync();
    }

    private string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        bool inQuotes = false;

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        result.Add(current.ToString());
        return result.ToArray();
    }
}