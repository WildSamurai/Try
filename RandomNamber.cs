using System;

class GuessTheNumber
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== GUESS THE NUMBER GAME ===");
        Console.WriteLine("I'm thinking of a number between 1 and 100.");
        Console.WriteLine("Try to guess it! I'll give you hints.\n");

        PlayGame();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void PlayGame()
    {
        
        Random random = new Random();
        int secretNumber = random.Next(1, 101);
        int attempts = 0;
        int guess = 0;

        Console.WriteLine("I've chosen a number. Let's begin!\n");

        
        while (guess != secretNumber)
        {
            attempts++;
            Console.Write($"Attempt #{attempts}: Enter your guess: ");

           
            if (!int.TryParse(Console.ReadLine(), out guess))
            {
                Console.WriteLine("Please enter a valid number!");
                attempts--; 
                continue;
            }

           
            if (guess < secretNumber)
            {
                Console.WriteLine("Too low! Try a higher number.");
            }
            else if (guess > secretNumber)
            {
                Console.WriteLine("Too high! Try a lower number.");
            }
            else
            {
                Console.WriteLine($"\nCongratulations! You guessed it!");
                Console.WriteLine($"The number was indeed {secretNumber}");
                Console.WriteLine($"It took you {attempts} attempts.");
            }

            Console.WriteLine(); 
        }
    }
}