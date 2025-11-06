using BookLibrary.Data;
using BookLibrary.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Starting Book Library Application...");

            var services = new ServiceCollection();

            string connectionString = GetSqlServerConnectionString();

            Console.WriteLine($"Using connection string: {connectionString}");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<ApplicationService>();

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                Console.WriteLine("Initializing database...");
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                try
                {
                    var timeout = Task.Delay(TimeSpan.FromSeconds(30));
                    var initTask = context.Database.EnsureCreatedAsync();

                    var completedTask = await Task.WhenAny(initTask, timeout);
                    if (completedTask == timeout)
                    {
                        throw new TimeoutException("Database initialization timed out after 30 seconds");
                    }

                    await initTask;
                    Console.WriteLine(" Database initialized successfully!");
                }
                catch (Exception dbEx)
                {
                    Console.WriteLine($" Database initialization failed: {dbEx.Message}");
                    Console.WriteLine("Please check your SQL Server connection and try again.");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }
            }

            Console.WriteLine("Starting application menu...");
            var app = serviceProvider.GetRequiredService<ApplicationService>();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Application error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    static string GetSqlServerConnectionString()
    {
        var connectionStrings = new[]
        {
            "Server=(localdb)\\mssqllocaldb;Database=BookLibrary;Trusted_Connection=true;TrustServerCertificate=true;",

            "Server=.\\SQLEXPRESS;Database=BookLibrary;Trusted_Connection=true;TrustServerCertificate=true;",

            "Server=localhost;Database=BookLibrary;Trusted_Connection=true;TrustServerCertificate=true;",

            "Server=localhost,1433;Database=BookLibrary;Trusted_Connection=true;TrustServerCertificate=true;",

            $"Server={Environment.MachineName};Database=BookLibrary;Trusted_Connection=true;TrustServerCertificate=true;"
        };

        foreach (var connStr in connectionStrings)
        {
            if (TestConnection(connStr))
            {
                Console.WriteLine($" Using connection: {connStr}");
                return connStr;
            }
        }

        Console.WriteLine(" Could not establish connection, using default...");
        return connectionStrings[0];
    }

    static bool TestConnection(string connectionString)
    {
        try
        {
            using var context = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(connectionString)
                    .Options);

            var canConnect = context.Database.CanConnect();
            return canConnect;
        }
        catch
        {
            return false;
        }
    }
}