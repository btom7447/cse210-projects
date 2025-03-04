using System;

class Program
{
    static void Main(string[] args)
    {
        string line = "=====================================";
        Console.WriteLine(line);
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();
        Console.WriteLine("");
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();
        Console.WriteLine("");
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
        Console.WriteLine("");
        Console.WriteLine(line);
    }
}
