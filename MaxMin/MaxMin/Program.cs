namespace LineSumCalculator;

using FileLineProcessor;
public class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            ShowMenu();
        }
        else
        {
            string filePath = args[0];
            CalculationStrategy strategy = args.Length > 1 && args[1] == "-min"
                ? CalculationStrategy.MinSum
                : CalculationStrategy.MaxSum;

            ProcessFile(filePath, strategy);
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("=== Line Sum Calculator ===");
        Console.WriteLine("1. Process file");
        Console.WriteLine("2. Exit");
        Console.Write("Choose option: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter file path: ");
                string filePath = Console.ReadLine();
                Console.Write("Find minimum? (y/n): ");
                bool findMin = Console.ReadLine().ToLower() == "y";
                var strategy = findMin ? CalculationStrategy.MinSum : CalculationStrategy.MaxSum;
                ProcessFile(filePath, strategy);
                break;
            case "2":
                return;
            default:
                Console.WriteLine("Invalid option");
                break;
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        ShowMenu();
    }

    static void ProcessFile(string filePath, CalculationStrategy strategy)
    {
        try
        {
            var processor = new FileLineProcessor(filePath, strategy);

            Console.WriteLine($"\nFile: {System.IO.Path.GetFileName(filePath)}");

            if (processor.IsEmptyFile())
            {
                Console.WriteLine("The file is empty.");
                return;
            }

            Console.WriteLine(processor.GetTargetLineInfo());
            Console.WriteLine(processor.GetWrongLinesInfo());

            Console.WriteLine($"\nTotal valid lines: {processor.GetValidLines().Count}");
            Console.WriteLine($"Total wrong lines: {processor.GetWrongLines().Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing file: {ex.Message}");
        }
    }
}