namespace BookLibrary.Services;

using BookLibrary.Models;

public interface IFilterService
{
    Task<Filter?> LoadFilterAsync(string filePath = "filter.json");
    Task<bool> SaveFilterAsync(Filter filter, string filePath = "filter.json");
}