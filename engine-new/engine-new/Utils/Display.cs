namespace orion;

internal class Display
{
    public static ConsoleColor Gray = ConsoleColor.Gray;
    public static ConsoleColor DarkGray = ConsoleColor.DarkGray;
    public static ConsoleColor PrimaryColour = Engine.IsDarkMode ? ConsoleColor.White : ConsoleColor.Black;

    public static ConsoleColor BackgroundColour = Engine.IsDarkMode ? ConsoleColor.Black : ConsoleColor.White;

    public static ConsoleColor InfoColour = ConsoleColor.Cyan;
    public static ConsoleColor ErrorColour = ConsoleColor.Red;
    public static ConsoleColor WarningColour = Engine.IsDarkMode ? ConsoleColor.Yellow : ConsoleColor.DarkYellow;
    public static ConsoleColor PointerColour = Engine.IsDarkMode ? ConsoleColor.White : ConsoleColor.Black;
    public static void UpdateTheme()
    {
        PrimaryColour = Engine.IsDarkMode ? ConsoleColor.White : ConsoleColor.Black;
        Console.BackgroundColor = Engine.IsDarkMode ? ConsoleColor.Black : ConsoleColor.White;
        WarningColour = Engine.IsDarkMode ? ConsoleColor.Yellow : ConsoleColor.DarkYellow;
        PointerColour = Engine.IsDarkMode ? ConsoleColor.White : ConsoleColor.Black;
    }

    public static void Message(string message)
    {
        Console.ForegroundColor = InfoColour;
        Console.Write($"\n\n  {message}");
        Console.ReadKey();
    }

    public static void Warning(string message)
    {
        Console.ForegroundColor = WarningColour;
        Console.Write($"\n\n  {message}");
        Console.ReadKey();
    }

    public static void Error(string message)
    {
        Console.ForegroundColor = ErrorColour;
        Console.Write($"\n\n  {message}");
        Console.ReadKey();
    }

    public static void LoadingIcon(int repetitions)
    {
        Console.ForegroundColor = PrimaryColour;
        string[] loadingSymbols = { "|", "/", "-", "\\" };

        Console.WriteLine();

        int currentIndex = 0;
        for (int iteration = 0; iteration < repetitions; iteration++)
        {
            Console.Write(" \r" + loadingSymbols[currentIndex++ % loadingSymbols.Length]);
            Thread.Sleep(100);
        }
    }

    public static string Menu(List<string> options, string header)
    {
        int activeOptionIndex = 0;
        bool selectedOption = false;
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = PrimaryColour;
            Console.WriteLine($"{header}\n");

            for (int i = 0; i < options.Count; i++)
            {
                Console.ForegroundColor = activeOptionIndex == i ?
                    selectedOption ? PrimaryColour : Gray : DarkGray;

                Console.WriteLine(activeOptionIndex == i ? $"\n  > {options[i]}\n" : options[i]);

                if (activeOptionIndex == i && selectedOption)
                {
                    for (int j = i + 1; j < options.Count; j++)
                    {
                        Console.ForegroundColor = DarkGray;
                        Console.WriteLine($"{options[j]}");
                    }

                    Console.ForegroundColor = Gray;
                    return options[i];
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
            else if (input.Key == ConsoleKey.Tab)
            {
                return "";
            }
        }
    }
}
