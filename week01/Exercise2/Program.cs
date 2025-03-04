using System;

class Program
{
    static void Main(string[] args)
    {
        string line = "=====================================";
        Console.WriteLine(line);
        Console.WriteLine("");
        Console.Write("What is your grade percentage? ");
        string gradePercentage = Console.ReadLine();
        int grade = int.Parse(gradePercentage);

        // DETERMINE THE LETTER GRADE
        string letter = "";
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // DETERMINE SIGN (+, -, or none)
        string sign = "";
        int lastDigit = grade % 10; 
        if (letter != "F") 
        {
            if (lastDigit >= 7 && letter != "A") 
            {
                sign = "+";
            }
            else if (lastDigit < 3 && letter != "F") 
            {
                sign = "-";
            }
        }

        // EXCEPTIONS
        if (letter == "A" && lastDigit >= 7)
        {
            sign = ""; 
        }
        else if (letter == "F")
        {
            sign = "";
        }

        // PRINT GRADE & SIGN
        Console.WriteLine($"Your letter grade is: {letter}{sign}");
        Console.WriteLine("");

        // DETERMINE IF PASS OR FAIL
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! You passed!");
        }
        else
        {
            Console.WriteLine("Sorry, you failed.");
        }

        Console.WriteLine(line);
    }
}