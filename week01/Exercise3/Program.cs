using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        string line = "=====================================";
        bool playAgain = true;

        while (playAgain)
        {
            // RANDOM NUMBER FROM 1 - 100
            int magicNumber = random.Next(1, 101); 
            int guessCount = 0;
            int guess = 0;

            Console.WriteLine(line);
            Console.WriteLine("");
            Console.WriteLine("Welcome to the Guess My Number game!");
            Console.WriteLine("I've picked a magic number between 1 and 100. Can you guess it?");

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                    Console.WriteLine("");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("You guessed it, Good Job!");
                    Console.WriteLine("");
                }
            }
            Console.WriteLine($"It took you {guessCount} guesses to find the magic number.");
            Console.WriteLine("");

            // PLAY AGAIN?
            Console.Write("Do you want to play again? (yes/no): ");
            string response = Console.ReadLine().ToLower();
            Console.WriteLine("");

            if (response != "yes")
            {
                playAgain = false;
            }
        }

        Console.WriteLine("Thanks for playing! Goodbye!");
        Console.WriteLine(line);
    }
}