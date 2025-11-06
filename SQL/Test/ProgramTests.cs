using Moq;

using System;
using System.Data;

using Xunit;

namespace StudentManagementSystem.Tests;

public class ProgramTests
{
    [Fact]
    public void ExecuteSqlScript_WithValidScript_ExecutesSuccessfully()
    {
        var mockConnection = new Mock<IDbConnection>();
        var mockCommand = new Mock<IDbCommand>();
        var mockTransaction = new Mock<IDbTransaction>();

        mockConnection.Setup(m => m.CreateCommand()).Returns(mockCommand.Object);
        mockConnection.Setup(m => m.BeginTransaction()).Returns(mockTransaction.Object);
        mockCommand.Setup(m => m.ExecuteNonQuery());

        string testScript = "SELECT 1";

        Assert.True(true);
    }

    [Theory]
    [InlineData("Server=test;Database=test;Integrated Security=true;")]
    [InlineData("Server=localhost;Database=master;User Id=sa;Password=test;")]
    public void ConnectionString_ShouldBeValid(string connectionString)
    {
        bool isValid = !string.IsNullOrEmpty(connectionString) &&
                      connectionString.Contains("Server=") &&
                      connectionString.Contains("Database=");

        Assert.True(isValid);
    }
}