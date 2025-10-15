
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matrixx;

namespace MatrixTests;
[TestClass]
public class MatrixTests
{
    [TestMethod]
    public void Constructor_ValidDimensions_CreatesMatrix()
    {
        var matrix = new Matrix(3, 4);

        Assert.AreEqual(3, matrix.Rows);
        Assert.AreEqual(4, matrix.Columns);
    }

    [TestMethod]
    public void Constructor_InvalidDimensions_ThrowsArgumentException()
    {
        Assert.ThrowsException<ArgumentException>(() => new Matrix(0, 5));
    }

    [TestMethod]
    public void Constructor_WithData_CreatesMatrixWithCorrectData()
    {
        var data = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };

        var matrix = new Matrix(data);

        Assert.AreEqual(2, matrix.Rows);
        Assert.AreEqual(3, matrix.Columns);
        Assert.AreEqual(1, matrix[0, 0]);
        Assert.AreEqual(6, matrix[1, 2]);
    }

    [TestMethod]
    public void Constructor_WithNullData_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new Matrix(null));
    }

    [TestMethod]
    public void Indexer_GetAndSet_WorksCorrectly()
    {
        var matrix = new Matrix(2, 2);

        matrix[0, 0] = 10;
        matrix[1, 1] = 20;

        Assert.AreEqual(10, matrix[0, 0]);
        Assert.AreEqual(20, matrix[1, 1]);
    }

    [TestMethod]
    public void Indexer_InvalidIndex_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix(2, 2);

        Assert.ThrowsException<IndexOutOfRangeException>(() =>
        {
            var value = matrix[5, 5];
        });
    }

    [TestMethod]
    public void CalculateTrace_SquareMatrix_ReturnsCorrectTrace()
    {
        var data = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        var matrix = new Matrix(data);

        var trace = matrix.CalculateTrace();

        Assert.AreEqual(15, trace); 
    }

    [TestMethod]
    public void CalculateTrace_RectangularMatrix_ReturnsCorrectTrace()
    {
        var data = new int[,]
        {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 }
        };
        var matrix = new Matrix(data);

        var trace = matrix.CalculateTrace();

        Assert.AreEqual(7, trace);
    }

    [TestMethod]
    public void GetSnailOrder_3x3Matrix_ReturnsCorrectOrder()
    {
        var data = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        var matrix = new Matrix(data);
        var expected = new int[] { 1, 2, 3, 6, 9, 8, 7, 4, 5 };

        var result = matrix.GetSnailOrder();

        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetSnailOrder_2x3Matrix_ReturnsCorrectOrder()
    {
        var data = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
        var matrix = new Matrix(data);
        var expected = new int[] { 1, 2, 3, 6, 5, 4 };

        var result = matrix.GetSnailOrder();

        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ToString_2x2Matrix_ReturnsFormattedString()
    {
        var data = new int[,]
        {
            { 1, 2 },
            { 3, 4 }
        };
        var matrix = new Matrix(data);
        var expected = "   1   2\r\n   3   4\r\n";

        var result = matrix.ToString();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetData_ReturnsCloneOfData()
    {
        var originalData = new int[,]
        {
            { 1, 2 },
            { 3, 4 }
        };
        var matrix = new Matrix(originalData);

        var clonedData = matrix.GetData();

        CollectionAssert.AreEqual(originalData, clonedData);
        Assert.AreNotSame(originalData, clonedData);
    }
}
