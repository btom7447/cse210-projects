using System;

class Program
{
    static void Main(string[] args)
    {
        // CALL FUNCTIONS IN ORDER
        DisplayWelcome();
        string userName = PromptUserName();
        int userNumber = PromptUserNumber();
        int squaredNumber = SquareNumber(userNumber);
        DisplayResult(userName, squaredNumber);
    }

    // WELCOME MESSAGE
    static void DisplayWelcome()
    {
        string line = "=========================================";
        Console.WriteLine(line);
        Console.WriteLine("");
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }

    static int SquareNumber(int number)
    {
        return number * number;
    }

    static void DisplayResult(string name, int squaredNumber)
    {
        string line = "========================================="; // Define line here
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
        Console.WriteLine("");
        Console.WriteLine(line);
    }
}