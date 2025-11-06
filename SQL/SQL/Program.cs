using Microsoft.Data.SqlClient;

using System;
using System.IO;
using System.Reflection;

namespace StudentManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Starting Student Management System...");
            Console.WriteLine("Searching for available SQL Server instances...");

            string masterConnectionString = FindWorkingConnectionString();
            if (masterConnectionString == null)
            {
                Console.WriteLine("No SQL Server instance found. Please install SQL Server Express.");
                Console.WriteLine("Download from: https://www.microsoft.com/en-us/sql-server/sql-server-downloads");
                return;
            }

            Console.WriteLine($"Using connection: {masterConnectionString}");

            ExecuteSqlScript(masterConnectionString, "create_database.sql");
            Console.WriteLine("Database and tables created successfully.");

            string appConnectionString = masterConnectionString.Replace("Database=master", "Database=StudentManagement");

            ExecuteSqlScript(appConnectionString, "fill_test_data.sql");
            Console.WriteLine("Test data inserted successfully.");

            Console.WriteLine("\n Groups with less than 10 students ");
            ExecuteQueryFromFile(appConnectionString, "query1_groups_less_than_10.sql");

            Console.WriteLine("\n Courses and Related Students ");
            ExecuteQueryFromFile(appConnectionString, "query3_courses_and_students.sql");

            Console.WriteLine("\n Deleting students from group SR-01 ");
            ExecuteNonQueryFromFile(appConnectionString, "query2_delete_students_sr01.sql");
            Console.WriteLine("Students from group SR-01 deleted successfully.");

            Console.WriteLine("\n Courses and Students After Deletion ");
            ExecuteQueryFromFile(appConnectionString, "query3_courses_and_students.sql");

            Console.WriteLine("\n Groups with less than 10 students (After Deletion) ");
            ExecuteQueryFromFile(appConnectionString, "query1_groups_less_than_10.sql");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static string FindWorkingConnectionString()
    {
        string[] possibleServers = {
            @".\SQLEXPRESS",  
            "(local)",       
            "localhost",      
            ".",             
            "127.0.0.1"       
        };

        foreach (string server in possibleServers)
        {
            string connectionString = $"Server={server};Database=master;Integrated Security=true;TrustServerCertificate=true;";
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine($" Connected successfully to: {server}");
                    return connectionString;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Failed to connect to: {server} - {ex.Message}");
            }
        }

        return null;
    }

    private static string GetScriptPath(string fileName)
    {
        string[] possiblePaths = {
            Path.Combine("SQLScripts", fileName),
            Path.Combine(Directory.GetCurrentDirectory(), "SQLScripts", fileName),
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SQLScripts", fileName),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQLScripts", fileName),
            fileName
        };

        foreach (string path in possiblePaths)
        {
            if (File.Exists(path))
            {
                Console.WriteLine($"Found script at: {path}");
                return path;
            }
        }

        Console.WriteLine("Could not find script file. Checked paths:");
        foreach (string path in possiblePaths)
        {
            Console.WriteLine($"  - {path}");
        }

        throw new FileNotFoundException($"Script file '{fileName}' not found.");
    }

    static void ExecuteSqlScript(string connectionString, string scriptFileName)
    {
        string scriptPath = GetScriptPath(scriptFileName);
        string script = File.ReadAllText(scriptPath);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string[] commands = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string command in commands)
            {
                if (!string.IsNullOrWhiteSpace(command))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(command.Trim(), connection))
                    {
                        try
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error executing command: {command.Substring(0, Math.Min(50, command.Length))}...");
                            Console.WriteLine($"Error details: {ex.Message}");
                        }
                    }
                }
            }
        }
    }

    static void ExecuteQueryFromFile(string connectionString, string queryFileName)
    {
        string queryPath = GetScriptPath(queryFileName);
        string query = File.ReadAllText(queryPath);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i),-20} ");
                    }
                    Console.WriteLine();
                    Console.WriteLine(new string('-', reader.FieldCount * 20));

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader[i],-20} ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }

    static void ExecuteNonQueryFromFile(string connectionString, string queryFileName)
    {
        string queryPath = GetScriptPath(queryFileName);
        string query = File.ReadAllText(queryPath);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
        }
    }
}