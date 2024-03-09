namespace orion;

internal class Display
{
    public static ConsoleColor Gray = ConsoleColor.Gray;
    public static ConsoleColor DarkGray = ConsoleColor.DarkGray;
    public static ConsoleColor White = ConsoleColor.White;
    public static ConsoleColor ErrorColour = ConsoleColor.Red;
    public static ConsoleColor InfoColour = ConsoleColor.Yellow;
    public static void LoadingIcon(int repetitions)
    {
        Console.ForegroundColor = White;
        string[] loadingSymbols = { "|", "/", "-", "\\" };

        Console.WriteLine();

        int currentIndex = 0;
        for (int iteration = 0; iteration < repetitions; iteration++)
        {
            Console.Write(" \r" + loadingSymbols[currentIndex++ % loadingSymbols.Length]);
            Thread.Sleep(100);
        }
    }

    public static void ErrorMessage(string message)
    {
        Console.ForegroundColor = ErrorColour;
        Console.Write($"\n\n  {message}");
        Console.ReadKey();
    }

    public static void Message(string message)
    {
        Console.ForegroundColor = InfoColour;
        Console.Write($"\n\n  {message}");
        Console.ReadKey();
    }


    public static string Menu(List<string> options, string header)
    {
        int activeOptionIndex = 0;
        bool selectedOption = false;

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = White;
            Console.WriteLine($"{header}\n");

            for (int i = 0; i < options.Count; i++)
            {
                Console.ForegroundColor = activeOptionIndex == i ?
                    (selectedOption ? White : Gray) : DarkGray;

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
