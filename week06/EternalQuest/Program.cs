using System;

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent(ref int totalPoints, ref int level);
    public abstract bool IsComplete();
    public abstract string GetStatus();
    public abstract string Serialize();

    public static Goal Deserialize(string data)
    {
        var parts = data.Split('|');
        var type = parts[0];

        return type switch
        {
            "Simple" => new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4])),
            "Eternal" => new EternalGoal(parts[1], parts[2], int.Parse(parts[3])),
            "Checklist" => new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])),
            _ => null,
        };
    }

    public string Name => _name;
    public string Description => _description;
}

class SimpleGoal : Goal
{
    private bool _completed;

    public SimpleGoal(string name, string description, int points, bool completed = false)
        : base(name, description, points)
    {
        _completed = completed;
    }

    public override void RecordEvent(ref int totalPoints, ref int level)
    {
        if (!_completed)
        {
            totalPoints += _points;
            _completed = true;
            CheckLevelUp(ref level, totalPoints);
            Console.WriteLine($"You earned {_points} points!");
        }
        else
        {
            Console.WriteLine("This goal is already completed.");
        }
    }

    public override bool IsComplete() => _completed;

    public override string GetStatus() => _completed ? "[X]" : "[ ]";

    public override string Serialize() => $"Simple|{_name}|{_description}|{_points}|{_completed}";

    protected void CheckLevelUp(ref int level, int totalPoints)
    {
        int newLevel = (totalPoints / 1000) + 1;
        if (newLevel > level)
        {
            Console.WriteLine($"You leveled up to Level {newLevel}!");
            level = newLevel;
        }
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent(ref int totalPoints, ref int level)
    {
        totalPoints += _points;
        CheckLevelUp(ref level, totalPoints);
        Console.WriteLine($"You earned {_points} points for recording eternal progress!");
    }

    public override bool IsComplete() => false;

    public override string GetStatus() => "[∞]";

    public override string Serialize() => $"Eternal|{_name}|{_description}|{_points}";

    protected void CheckLevelUp(ref int level, int totalPoints)
    {
        int newLevel = (totalPoints / 1000) + 1;
        if (newLevel > level)
        {
            Console.WriteLine($"You leveled up to Level {newLevel}!");
            level = newLevel;
        }
    }
}

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int currentCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _currentCount = currentCount;
        _bonus = bonus;
    }

    public override void RecordEvent(ref int totalPoints, ref int level)
    {
        if (_currentCount < _targetCount)
        {
            _currentCount++;
            totalPoints += _points;

            if (_currentCount == _targetCount)
            {
                totalPoints += _bonus;
                Console.WriteLine($"Goal completed! Bonus {_bonus} points awarded!");
            }
            else
            {
                Console.WriteLine($"Progress made! {_points} points awarded.");
            }

            CheckLevelUp(ref level, totalPoints);
        }
        else
        {
            Console.WriteLine("This checklist goal has already been completed.");
        }
    }

    public override bool IsComplete() => _currentCount >= _targetCount;

    public override string GetStatus() => $"[{(_currentCount >= _targetCount ? "X" : " ")}] Completed {_currentCount}/{_targetCount}";

    public override string Serialize() => $"Checklist|{_name}|{_description}|{_points}|{_targetCount}|{_currentCount}|{_bonus}";

    protected void CheckLevelUp(ref int level, int totalPoints)
    {
        int newLevel = (totalPoints / 1000) + 1;
        if (newLevel > level)
        {
            Console.WriteLine($"You leveled up to Level {newLevel}!");
            level = newLevel;
        }
    }
}

class Program
{
    private static readonly List<Goal> goals = new();
    private static int totalPoints = 0;
    private static int level = 1;

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("\n Eternal Quest Menu ");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Show Score and Level");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");

            Console.Write("Select an option: ");
            Console.WriteLine(" \n================================= \n");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordGoal(); break;
                case "4": ShowStats(); break;
                case "5": SaveGoals(); break;
                case "6": LoadGoals(); break;
                case "7": running = false; break;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("\n=================================");
        Console.WriteLine("\nChoose goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("================================= \n");
        string choice = Console.ReadLine();

        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string desc = Console.ReadLine();
        Console.Write("Enter point value: ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                goals.Add(new SimpleGoal(name, desc, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, desc, points));
                break;
            case "3":
                Console.Write("How many times to complete? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points upon completion: ");
                int bonus = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, desc, points, target, 0, bonus));
                break;
        }

        Console.WriteLine("🎯 Goal added!");
    }

    static void ListGoals()
    {
        Console.WriteLine("\n=== Your Goals ===");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()} {goals[i].Name} - {goals[i].Description}");
        }
    }

    static void RecordGoal()
    {
        ListGoals();
        Console.Write("\nEnter goal number to record: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < goals.Count)
        {
            goals[index].RecordEvent(ref totalPoints, ref level);
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    static void ShowStats()
    {
        Console.WriteLine($"\nTotal Points: {totalPoints}");
        Console.WriteLine($"Level: {level}");
    }

    static void SaveGoals()
    {
        using StreamWriter writer = new("goals.txt");
        writer.WriteLine(totalPoints);
        writer.WriteLine(level);
        foreach (Goal g in goals)
        {
            writer.WriteLine(g.Serialize());
        }
        Console.WriteLine("Goals saved.");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            goals.Clear();
            var lines = File.ReadAllLines("goals.txt");
            totalPoints = int.Parse(lines[0]);
            level = int.Parse(lines[1]);

            for (int i = 2; i < lines.Length; i++)
            {
                var goal = Goal.Deserialize(lines[i]);
                if (goal != null)
                    goals.Add(goal);
            }

            Console.WriteLine("Goals loaded.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}