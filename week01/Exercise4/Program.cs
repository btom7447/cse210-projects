using System;

class Program
{
    static void Main(string[] args)
    {
        string line = "=========================================";
        List<int> numbers = new List<int>();
        Console.WriteLine(line);
        Console.WriteLine("");
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());

            if (number == 0)
            {
                break;
            }

            numbers.Add(number); 
        }

        // CORE REQUIREMENT
        int sum = numbers.Sum();
        double average = numbers.Average();
        int max = numbers.Max();

        Console.WriteLine("");
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");
        Console.WriteLine("");

        // STRETCH CHALLENGES
        int smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty(0).Min();
        List<int> sortedNumbers = numbers.OrderBy(n => n).ToList();

        Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        Console.WriteLine("The sorted list is:");
        Console.WriteLine("");
        foreach (int num in sortedNumbers)
        {
            Console.WriteLine(num);
        }

        Console.WriteLine("");
        Console.WriteLine(line);
    }
}