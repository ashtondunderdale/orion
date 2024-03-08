using System.Diagnostics;
using System.Drawing;

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

        DisplayMenuOptions(new List<string>() { "Create", "View", "Load", "Delete" }, "  < Orion >  ");
        DisplayLoadingIcon(10);
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
                Console.ForegroundColor = activeOptionIndex == i ? 
                    (selectedOption ? ConsoleColor.White : ConsoleColor.Gray) : ConsoleColor.DarkGray;

                Console.WriteLine(activeOptionIndex == i ? $"\n > {options[i]}\n" : options[i]);

                if (activeOptionIndex == i && selectedOption) return;
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

    static void DisplayLoadingIcon(int repetitions)
    {
        Console.ForegroundColor = ConsoleColor.White;
        string[] loadingSymbols = { "|", "/", "-", "\\" };

        int currentIndex = 0;
        for (int iteration = 0; iteration < repetitions; iteration++)
        {
            Console.Write(" \r" + loadingSymbols[currentIndex++ % loadingSymbols.Length]);
            Thread.Sleep(100);
        }
    }

}

