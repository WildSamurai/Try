using System;

namespace count
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("choose operation ( /, *, -, +): ");
                string operation = Console.ReadLine();

                if (operation != "/" && operation != "*" && operation != "-" && operation != "+")
                {
                    Console.WriteLine("You choose incorrect symbol");
                    Console.WriteLine("press any button to continue");
                    Console.ReadKey();
                    Console.Clear();
                    continue; 
                }

                Console.WriteLine("type first number: ");
                int a = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("type second number: ");
                int b = Convert.ToInt32(Console.ReadLine());

                
                if (operation == "*")
                {
                    Console.WriteLine(a * b);
                }
                else if (operation == "+")
                {
                    Console.WriteLine(a + b);
                }
                else if (operation == "-")
                {
                    Console.WriteLine(a - b);
                }
                else if (operation == "/")
                {
                    
                    if (b == 0)
                    {
                        Console.WriteLine("Error: Division by zero!");
                    }
                    else
                    {
                        Console.WriteLine(a / b);
                    }
                }

                Console.WriteLine("press any button to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}