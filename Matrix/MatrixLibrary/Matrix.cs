namespace Matrixx;

using System;
using System.Text;

public class Matrix
{
    private readonly int[,] _data;
    private readonly Random _random;

    public int Rows { get; }
    public int Columns { get; }

    public int this[int row, int col]
    {
        get => _data[row, col];
        set => _data[row, col] = value;
    }

    public Matrix(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
            throw new ArgumentException("Rows and columns must be positive integers");

        Rows = rows;
        Columns = columns;
        _data = new int[rows, columns];
        _random = new Random();

        FillWithRandomData();
    }

    public Matrix(int[,] data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        Rows = data.GetLength(0);
        Columns = data.GetLength(1);
        _data = (int[,])data.Clone();
        _random = new Random();
    }

    private void FillWithRandomData()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                _data[i, j] = _random.Next(0, 101);
            }
        }
    }

    public int CalculateTrace()
    {
        int trace = 0;
        int minDimension = Math.Min(Rows, Columns);

        for (int i = 0; i < minDimension; i++)
        {
            trace += _data[i, i];
        }

        return trace;
    }

    public int[] GetSnailOrder()
    {
        if (Rows == 0 || Columns == 0)
            return Array.Empty<int>();

        int[] result = new int[Rows * Columns];
        int index = 0;

        int top = 0, bottom = Rows - 1;
        int left = 0, right = Columns - 1;

        while (top <= bottom && left <= right)
        {
            for (int j = left; j <= right; j++)
            {
                result[index++] = _data[top, j];
            }
            top++;

            for (int i = top; i <= bottom; i++)
            {
                result[index++] = _data[i, right];
            }
            right--;

            if (top <= bottom)
            {
                for (int j = right; j >= left; j--)
                {
                    result[index++] = _data[bottom, j];
                }
                bottom--;
            }

            if (left <= right)
            {
                for (int i = bottom; i >= top; i--)
                {
                    result[index++] = _data[i, left];
                }
                left++;
            }
        }

        return result;
    }

    public void PrintWithHighlightedDiagonal(ConsoleColor diagonalColor = ConsoleColor.Green)
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (i == j)
                {
                    Console.ForegroundColor = diagonalColor;
                    Console.Write($"{_data[i, j],4}");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"{_data[i, j],4}");
                }
            }
            Console.WriteLine();
        }
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                result.Append($"{_data[i, j],4}");
            }
            result.AppendLine();
        }

        return result.ToString();
    }

    public int[,] GetData() => (int[,])_data.Clone();
}