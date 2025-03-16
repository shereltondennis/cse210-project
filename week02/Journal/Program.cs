using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public Entry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Date}: {Prompt}\n{Response}\n";
    }
}

public class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private static Random _random = new Random();
    private List<string> _prompts = new List<string>
    {
        "Who have I every made happy before?",
        "What is the best part of this course CSE210 I loved?",
        "How did I see the hand of the Lord in my life this year?",
        "What was the strongest emotion I have ever felt?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void AddEntry()
    {
        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        _entries.Add(new Entry(prompt, response));
        Console.WriteLine("Entry added successfully!");
    }

    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries available.");
            return;
        }

        foreach (var entry in _entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in _entries)
            {
                writer.WriteLine(entry.ToString());
            }
        }
        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadFromFile()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        for (int i = 0; i < lines.Length; i += 3)
        {
            string dateString = lines[i].Split(':')[0];
            DateTime date = DateTime.Parse(dateString);
            string prompt = lines[i].Substring(lines[i].IndexOf(':') + 2);
            string response = lines[i + 1];
            _entries.Add(new Entry(prompt, response) { Date = date });
        }
        Console.WriteLine("Journal loaded successfully!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Save the journal to a file");
            Console.WriteLine("3. Display the journal" );
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveToFile();
                    break;
                case "4":
                    journal.LoadFromFile();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        Console.WriteLine("Goodbye!");
    }
}
