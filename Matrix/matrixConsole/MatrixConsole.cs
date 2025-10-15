namespace MatrixConsole;

using System;

using Matrixx;

public class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Enter matrix dimensions:");
            Console.Write("Number of rows: ");
            var rows = int.Parse(Console.ReadLine());

            Console.Write("Number of columns: ");
            var columns = int.Parse(Console.ReadLine());

            var matrix = new Matrix(rows, columns);

            Console.WriteLine("\nGenerated matrix:");
            matrix.PrintWithHighlightedDiagonal(ConsoleColor.Green);

            var trace = matrix.CalculateTrace();
            Console.WriteLine($"\nMatrix trace: {trace}");

            var snail = matrix.GetSnailOrder();
            Console.WriteLine("Elements in snail order:");
            Console.WriteLine(string.Join(" ", snail));

            Console.WriteLine($"\nElement at [0,0]: {matrix[0, 0]}");

            Console.WriteLine("\nMatrix as string:");
            Console.WriteLine(matrix);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}