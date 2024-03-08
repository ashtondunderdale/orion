using System.Diagnostics;

namespace orion;

internal class Launcher
{
    static void Main()
    {
        Initialise();
    }

    static void Initialise() 
    {
        Console.WriteLine("Loading Projects...");
        Console.ReadKey();

        DisplayMenuOptions(new List<string>() { "Create", "Load", "Delete" }, " --> Orion <--");
    }

    static void DisplayMenuOptions(List<string> options, string header)
    {
        int activeOptionIndex = 0;
        bool selectedOption = false;

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{header}\n");

            for (int i = 0; i < options.Count; i++)
            {
                if (activeOptionIndex == i)
                {
                    if (selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\n > {options[i]}\n");

                        DisplayLoadingIcon();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"\n > {options[i]}\n");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(options[i]);
                }
            }

            ConsoleKeyInfo input = Console.ReadKey();

            if (input.Key == ConsoleKey.UpArrow && activeOptionIndex - 1 > -1)
            {
                activeOptionIndex--;               
            }
            else if (input.Key == ConsoleKey.DownArrow && activeOptionIndex < options.Count - 1)
            {
                activeOptionIndex++;              
            }
            else if (input.Key == ConsoleKey.Enter)
            {
                selectedOption = !selectedOption;
            }
        }
    }

    static void DisplayLoadingIcon()
    {
        Console.ForegroundColor = ConsoleColor.White;

        string[] loadingSymbols = { "|", "/", "-", "\\" };
        int currentIndex = 0;

        while (true)
        {
            Console.Write(" \r" + loadingSymbols[currentIndex]);
            currentIndex = (currentIndex + 1) % loadingSymbols.Length; 
            Thread.Sleep(100);
        }
    }
}

