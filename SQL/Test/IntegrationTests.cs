using Microsoft.Data.SqlClient;

using System;

using Xunit;

namespace StudentManagementSystem.Tests
{
    [Collection("DatabaseTests")]
    public class IntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public IntegrationTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void DatabaseConnection_ShouldBeSuccessful()
        {
            bool connectionSuccessful = false;
            string errorMessage = string.Empty;

            try
            {
                using (var connection = new SqlConnection(_fixture.ConnectionString))
                {
                    connection.Open();
                    connectionSuccessful = connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            Assert.True(connectionSuccessful, $"Database connection failed: {errorMessage}");
        }

        [Fact]
        public void AllTables_ShouldExist()
        {
            var tableCheckQuery = @"
                SELECT COUNT(*) 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME IN ('STUDENTS', 'GROUPS', 'COURSES')";

            int tableCount;
            using (var connection = new SqlConnection(_fixture.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(tableCheckQuery, connection))
                {
                    tableCount = (int)command.ExecuteScalar();
                }
            }

            Assert.Equal(3, tableCount);
        }

        [Fact]
        public void TestData_ShouldBeLoaded()
        {
            var studentCountQuery = "SELECT COUNT(*) FROM STUDENTS";
            var groupCountQuery = "SELECT COUNT(*) FROM GROUPS";
            var courseCountQuery = "SELECT COUNT(*) FROM COURSES";

            using (var connection = new SqlConnection(_fixture.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(studentCountQuery, connection))
                {
                    int studentCount = (int)command.ExecuteScalar();
                    Assert.True(studentCount > 0, "Should have test students");
                }

                using (var command = new SqlCommand(groupCountQuery, connection))
                {
                    int groupCount = (int)command.ExecuteScalar();
                    Assert.True(groupCount > 0, "Should have test groups");
                }

                using (var command = new SqlCommand(courseCountQuery, connection))
                {
                    int courseCount = (int)command.ExecuteScalar();
                    Assert.True(courseCount > 0, "Should have test courses");
                }
            }
        }
    }

    public class DatabaseFixture : IDisposable
    {
        public string ConnectionString { get; private set; }
        private readonly string _testDatabaseName = "StudentManagementIntegrationTest";

        public DatabaseFixture()
        {
            ConnectionString = $"Server=localhost;Database={_testDatabaseName};Integrated Security=true;";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var masterConnectionString = "Server=localhost;Database=master;Integrated Security=true;";

            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();

                var cleanupCommand = new SqlCommand(
                    $@"IF EXISTS (SELECT name FROM sys.databases WHERE name = '{_testDatabaseName}')
                       BEGIN
                           ALTER DATABASE [{_testDatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                           DROP DATABASE [{_testDatabaseName}];
                       END", connection);
                cleanupCommand.ExecuteNonQuery();

                var createCommand = new SqlCommand($"CREATE DATABASE [{_testDatabaseName}]", connection);
                createCommand.ExecuteNonQuery();
            }

            ExecuteSqlScript("SQLScripts\\create_database.sql");
            ExecuteSqlScript("SQLScripts\\fill_test_data.sql");
        }

        private void ExecuteSqlScript(string scriptPath)
        {
            string script = System.IO.File.ReadAllText(scriptPath);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string[] commands = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string command in commands)
                {
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(command.Trim(), connection))
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            var masterConnectionString = "Server=localhost;Database=master;Integrated Security=true;";

            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    $@"IF EXISTS (SELECT name FROM sys.databases WHERE name = '{_testDatabaseName}')
                       BEGIN
                           ALTER DATABASE [{_testDatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                           DROP DATABASE [{_testDatabaseName}];
                       END", connection);
                command.ExecuteNonQuery();
            }
        }
    }

    [CollectionDefinition("DatabaseTests")]
    public class DatabaseTestCollection : ICollectionFixture<DatabaseFixture>
    {
     
    }
}