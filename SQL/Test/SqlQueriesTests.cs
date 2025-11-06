using Microsoft.Data.SqlClient;

using System;
using System.IO;

using Xunit;

namespace StudentManagementSystem.Tests
{
    public class SqlQueriesTests : IDisposable
    {
        private readonly string _connectionString;
        private readonly string _testDatabaseName = "StudentManagementTest";

        public SqlQueriesTests()
        {
            _connectionString = $"Server=localhost;Database={_testDatabaseName};Integrated Security=true;";
            InitializeTestDatabase();
        }

        public void Dispose()
        {
            CleanupTestDatabase();
        }

        private void InitializeTestDatabase()
        {
            var masterConnectionString = "Server=localhost;Database=master;Integrated Security=true;";

            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();

                var dropDbCommand = new SqlCommand(
                    $@"IF EXISTS (SELECT name FROM sys.databases WHERE name = '{_testDatabaseName}')
                       BEGIN
                           ALTER DATABASE [{_testDatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                           DROP DATABASE [{_testDatabaseName}];
                       END", connection);
                dropDbCommand.ExecuteNonQuery();

                var createDbCommand = new SqlCommand($"CREATE DATABASE [{_testDatabaseName}]", connection);
                createDbCommand.ExecuteNonQuery();
            }

            ExecuteSqlScript("SQLScripts\\create_database.sql");
            ExecuteSqlScript("SQLScripts\\fill_test_data.sql");
        }

        private void CleanupTestDatabase()
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

        private void ExecuteSqlScript(string scriptPath)
        {
            string script = File.ReadAllText(scriptPath);
            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        [Fact]
        public void TestGroupsWithLessThan10Students_ReturnsCorrectGroups()
        {
            var query = @"
                SELECT 
                    g.GROUP_ID,
                    g.NAME as GROUP_NAME,
                    c.NAME as COURSE_NAME,
                    COUNT(s.STUDENT_ID) as STUDENT_COUNT
                FROM GROUPS g
                INNER JOIN COURSES c ON g.COURSE_ID = c.COURSE_ID
                LEFT JOIN STUDENTS s ON g.GROUP_ID = s.GROUP_ID
                GROUP BY g.GROUP_ID, g.NAME, c.NAME
                HAVING COUNT(s.STUDENT_ID) < 10";

            var results = new System.Collections.Generic.List<dynamic>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new
                            {
                                GroupId = reader["GROUP_ID"],
                                GroupName = reader["GROUP_NAME"].ToString(),
                                CourseName = reader["COURSE_NAME"].ToString(),
                                StudentCount = Convert.ToInt32(reader["STUDENT_COUNT"])
                            });
                        }
                    }
                }
            }

            Assert.NotEmpty(results);
            foreach (var result in results)
            {
                Assert.True(result.StudentCount < 10, $"Group {result.GroupName} should have less than 10 students");
            }
        }

        [Fact]
        public void TestDeleteStudentsFromGroupSR01_DeletesAllStudents()
        {
            var countQuery = @"
                SELECT COUNT(*) 
                FROM STUDENTS s
                INNER JOIN GROUPS g ON s.GROUP_ID = g.GROUP_ID
                WHERE g.NAME = 'SR-01'";

            var deleteQuery = @"
                DELETE FROM STUDENTS 
                WHERE GROUP_ID IN (SELECT GROUP_ID FROM GROUPS WHERE NAME = 'SR-01')";

            int initialCount;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(countQuery, connection))
                {
                    initialCount = (int)command.ExecuteScalar();
                }
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            int finalCount;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(countQuery, connection))
                {
                    finalCount = (int)command.ExecuteScalar();
                }
            }

            Assert.True(initialCount > 0); 
            Assert.Equal(0, finalCount); 
        }

        [Fact]
        public void TestCoursesAndStudents_ReturnsAllRelationships()
        {
            var query = @"
                SELECT 
                    c.NAME as COURSE_NAME,
                    g.NAME as GROUP_NAME,
                    s.FIRST_NAME,
                    s.LAST_NAME,
                    s.STUDENT_ID
                FROM COURSES c
                INNER JOIN GROUPS g ON c.COURSE_ID = g.COURSE_ID
                INNER JOIN STUDENTS s ON g.GROUP_ID = s.GROUP_ID
                ORDER BY c.NAME, g.NAME, s.LAST_NAME, s.FIRST_NAME";

            var results = new System.Collections.Generic.List<dynamic>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new
                            {
                                CourseName = reader["COURSE_NAME"].ToString(),
                                GroupName = reader["GROUP_NAME"].ToString(),
                                FirstName = reader["FIRST_NAME"].ToString(),
                                LastName = reader["LAST_NAME"].ToString(),
                                StudentId = Convert.ToInt32(reader["STUDENT_ID"])
                            });
                        }
                    }
                }
            }

            Assert.NotEmpty(results);

            foreach (var result in results)
            {
                Assert.False(string.IsNullOrEmpty(result.CourseName));
                Assert.False(string.IsNullOrEmpty(result.GroupName));
                Assert.False(string.IsNullOrEmpty(result.FirstName));
                Assert.False(string.IsNullOrEmpty(result.LastName));
                Assert.True(result.StudentId > 0);
            }
        }
    }
}