using System;
using System.Threading;

public abstract class MindfulnessActivity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }  

    public MindfulnessActivity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    // Method to display common start message
    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine("\n==============================");
        Console.WriteLine($"Starting {Name} activity...");
        Console.WriteLine(Description);
        Console.WriteLine($"Please set your duration in seconds:");
        int duration = int.Parse(Console.ReadLine());
        Duration = duration;
        Console.WriteLine("Prepare to begin...");
        Console.WriteLine("==============================\n");
        Thread.Sleep(3000);
    }

    // Common end message with completion feedback
    public void EndActivity(string completionMessage)
    {
        Console.Clear();
        Console.WriteLine("\n==============================");
        Console.WriteLine($"Great job completing the {Name} activity!");
        Console.WriteLine($"You did it for {Duration} seconds!");
        Console.WriteLine(completionMessage); 
        Console.WriteLine("==============================\n");
        Thread.Sleep(3000);
    }

    // Abstract method for activity-specific implementation
    public abstract void RunActivity();
}

public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by guiding you through deep breathing.") { }

    public override void RunActivity()
    {
        StartActivity();

        int remainingTime = Duration;
        string[] messages = { "Breathe in...", "Breathe out..." };

        while (remainingTime > 0)
        {
            foreach (var message in messages)
            {
                Console.Clear();
                Console.WriteLine("\n==============================");
                Console.WriteLine(message);
                Console.WriteLine($"Time remaining: {remainingTime} seconds");
                Console.WriteLine("==============================\n");
                Thread.Sleep(2000);  
                remainingTime -= 2;
            }
        }

        EndActivity("You have taken time to focus on your breath. Keep this peaceful energy with you!");
    }
}

public class ReflectionActivity : MindfulnessActivity
{
    private static Random random = new Random();
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "How did you feel when it was complete?",
        "What is your favorite thing about this experience?",
        "What did you learn about yourself?"
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times of strength and resilience.") { }

    public override void RunActivity()
    {
        StartActivity();

        int remainingTime = Duration;
        string selectedPrompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine(selectedPrompt);
        Thread.Sleep(3000);

        while (remainingTime > 0)
        {
            string question = questions[random.Next(questions.Length)];
            Console.Clear();
            Console.WriteLine(question);
            Thread.Sleep(3000);  

            remainingTime -= 3;
        }

        EndActivity("Reflecting on your strength helps you tap into your inner resilience. Well done!");
    }
}

public class ListingActivity : MindfulnessActivity
{
    private static Random random = new Random();
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "When have you felt the Holy Ghost this month?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by listing positive aspects.") { }

    public override void RunActivity()
    {
        StartActivity();

        int remainingTime = Duration;
        string selectedPrompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine(selectedPrompt);
        Thread.Sleep(3000);  

        int itemCount = 0;
        Console.WriteLine("Start listing your items (type 'done' to finish):");
        
        while (remainingTime > 0)
        {
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "done") break;
            itemCount++;
            remainingTime--;
        }

        Console.WriteLine($"You listed {itemCount} items.");
        EndActivity("Listing your blessings brings more gratitude into your life. Keep reflecting on these positive things!");
    }
}

public class Program
{
    // Logs for counting activity completions
    static int breathingCount = 0;
    static int reflectionCount = 0;
    static int listingCount = 0;

    public static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n==============================");
            Console.WriteLine("Welcome to the Mindfulness App");
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.WriteLine("==============================\n");

            int choice = int.Parse(Console.ReadLine());

            if (choice == 4) break;

            MindfulnessActivity activity = choice switch
            {
                1 => new BreathingActivity(),
                2 => new ReflectionActivity(),
                3 => new ListingActivity(),
                _ => null
            };

            if (activity != null)
            {
                activity.RunActivity();

                // Update activity completion count
                if (activity is BreathingActivity) breathingCount++;
                if (activity is ReflectionActivity) reflectionCount++;
                if (activity is ListingActivity) listingCount++;

                // Display activity completion stats
                Console.Clear();
                Console.WriteLine("\n==============================");
                Console.WriteLine("Activity Stats:");
                Console.WriteLine($"Breathing Activity Completed: {breathingCount} times");
                Console.WriteLine($"Reflection Activity Completed: {reflectionCount} times");
                Console.WriteLine($"Listing Activity Completed: {listingCount} times");
                Console.WriteLine("==============================\n");
                Thread.Sleep(3000);  
            }
        }
    }
}
