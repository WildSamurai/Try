namespace BookLibrary.Services;

using BookLibrary.Entities;
using BookLibrary.Models;

public interface IBookService
{
    Task<bool> AddBooksFromFileAsync(string filePath);
    Task<(int count, List<Book> books)> SearchBooksAsync(Filter filter);
    Task<bool> SaveBooksToFileAsync(List<Book> books, string filePath);
    Task<int> GetTotalBookCountAsync();
}