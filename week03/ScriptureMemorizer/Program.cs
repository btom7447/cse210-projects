using System;

class Program
{
    static void Main(string[] args)
    {
        string line = "=========================================";
        List<Scripture> scriptures = LoadScriptures("scriptures.txt");
        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found in the file. Please check 'scriptures.txt'.");
            return;
        }

        Random rand = new Random();
        Scripture scripture = scriptures[rand.Next(scriptures.Count)];

        while (!scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(line);
            Console.WriteLine("");
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");

            string input = Console.ReadLine();
            if (input.ToLower() == "quit") break;

            scripture.HideRandomWords(2);
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words are hidden. Goodbye!");
        Console.WriteLine("");
        Console.WriteLine(line);
    }

    static List<Scripture> LoadScriptures(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file '{filePath}' was not found.");
            return scriptures;
        }

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(" | ");
            if (parts.Length == 2)
            {
                Reference reference = new Reference(parts[0]);
                scriptures.Add(new Scripture(reference, parts[1]));
            }
        }
        return scriptures;
    }
}

// Handles scripture reference details
class Reference
{
    private string _text;

    public Reference(string text)
    {
        _text = text;
    }

    public string GetDisplayText() => _text;
}

// Represents a word in the scripture
class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide() => _isHidden = true;
    public bool IsHidden() => _isHidden;
    public string GetDisplayText() => _isHidden ? new string('_', _text.Length) : _text;
}

// Manages the scripture text and hiding words
class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int numberToHide)
    {
        Random rand = new Random();
        var visibleWords = _words.Where(word => !word.IsHidden()).ToList();

        if (visibleWords.Count == 0) return;

        int wordsToHide = Math.Min(numberToHide, visibleWords.Count);

        for (int i = 0; i < wordsToHide; i++)
        {
            int index = rand.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public string GetDisplayText()
    {
        return $"{_reference.GetDisplayText()}\n{string.Join(" ", _words.Select(w => w.GetDisplayText()))}";
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden());
    }
}