namespace BookLibrary.Services;

using System.Text.Json;

using BookLibrary.Models;

public class FilterService : IFilterService
{
    public async Task<Filter?> LoadFilterAsync(string filePath = "filter.json")
    {
        try
        {
            if (!File.Exists(filePath))
                return null;

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<Filter>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> SaveFilterAsync(Filter filter, string filePath = "filter.json")
    {
        try
        {
            var json = JsonSerializer.Serialize(filter, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await File.WriteAllTextAsync(filePath, json);
            return true;
        }
        catch
        {
            return false;
        }
    }
}