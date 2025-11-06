namespace BookLibrary.Tests.TestHelpers;

using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;

public static class TestDbContextFactory
{
    public static ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    public static async Task<ApplicationDbContext> CreateInMemoryDbContextWithData()
    {
        var context = CreateInMemoryDbContext();
        await SeedTestDataAsync(context);
        return context;
    }

    private static async Task SeedTestDataAsync(ApplicationDbContext context)
    {
        var fantasyGenre = new Entities.Genre { Id = Guid.NewGuid(), Name = "Fantasy" };
        var scifiGenre = new Entities.Genre { Id = Guid.NewGuid(), Name = "Sci-Fi" };

        var tolkienAuthor = new Entities.Author { Id = Guid.NewGuid(), Name = "J.R.R. Tolkien" };
        var asimovAuthor = new Entities.Author { Id = Guid.NewGuid(), Name = "Isaac Asimov" };

        var publisher1 = new Entities.Publisher { Id = Guid.NewGuid(), Name = "Test Publisher 1" };
        var publisher2 = new Entities.Publisher { Id = Guid.NewGuid(), Name = "Test Publisher 2" };

        var books = new[]
        {
            new Entities.Book
            {
                Id = Guid.NewGuid(),
                Title = "The Hobbit",
                Pages = 310,
                GenreId = fantasyGenre.Id,
                AuthorId = tolkienAuthor.Id,
                PublisherId = publisher1.Id,
                ReleaseDate = new DateTime(1937, 9, 21)
            },
            new Entities.Book
            {
                Id = Guid.NewGuid(),
                Title = "Foundation",
                Pages = 255,
                GenreId = scifiGenre.Id,
                AuthorId = asimovAuthor.Id,
                PublisherId = publisher2.Id,
                ReleaseDate = new DateTime(1951, 6, 1)
            },
            new Entities.Book
            {
                Id = Guid.NewGuid(),
                Title = "The Lord of the Rings",
                Pages = 1178,
                GenreId = fantasyGenre.Id,
                AuthorId = tolkienAuthor.Id,
                PublisherId = publisher1.Id,
                ReleaseDate = new DateTime(1954, 7, 29)
            }
        };

        await context.Genres.AddRangeAsync(fantasyGenre, scifiGenre);
        await context.Authors.AddRangeAsync(tolkienAuthor, asimovAuthor);
        await context.Publishers.AddRangeAsync(publisher1, publisher2);
        await context.Books.AddRangeAsync(books);

        await context.SaveChangesAsync();
    }
}