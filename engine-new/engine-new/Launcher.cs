namespace orion;

internal class Launcher
{
    static List<Project> Projects = new();

    static ConsoleColor Gray = ConsoleColor.Gray;
    static ConsoleColor DarkGray = ConsoleColor.DarkGray;
    static ConsoleColor White = ConsoleColor.White;
    static ConsoleColor ErrorColour = ConsoleColor.Yellow;
    static void Main()
    {
        Initialise();
    }

    static void Initialise() 
    {
        Console.WriteLine("Loading Projects...");
        Console.ReadKey();

        while (true) 
        {
            string option = DisplayMenuOptions(new List<string>() { "Create", "View", "Load", "Delete", "Settings" }, "  < Orion >  ");
            DisplayLoadingIcon(4);

            switch (option)
            {
                case "Create":
                    CreateProject();
                    break;

                case "View":
                    ViewProjects();
                    break;

                case "Load":
                    LoadProject();
                    break;

                case "Delete":
                    DeleteProject();
                    break;
            }
        }
    }

    static void CreateProject() 
    {
        while (true) 
        {
            Console.Clear();

            Console.ForegroundColor = Gray;
            Console.WriteLine("enter a name for your project\n\n  >");

            Console.ForegroundColor = DarkGray;
            Console.SetCursorPosition(4, 2);
            string? projectName = Console.ReadLine();

            if (string.IsNullOrEmpty(projectName))
            {
                DisplayErrorMessage("project name can not be empty");
                continue;
            }

            if (projectName.Length > 16) 
            {
                DisplayErrorMessage("project name must be less than 16 characters");
                continue;
            }

            Project project = new(projectName, DateTime.Now);
            Projects.Add(project);

            // "would you like to view this project now?"

            return;
        }
    }

    static void ViewProjects()
    {
        Console.Clear();

        Console.ForegroundColor = Gray;
        Console.WriteLine("projects\n\n");

        for (int i = 0; i < Projects.Count; i++)
        {
            Console.ForegroundColor = White;
            Console.Write($" {Projects[i].Name,-16}   ");

            Console.ForegroundColor = DarkGray;
            Console.Write($"{Projects[i].CreationDate}\n");
        }

        Console.ReadKey();
    }

    static void LoadProject()
    {

    }

    static void DeleteProject()
    {

    }

    static string DisplayMenuOptions(List<string> options, string header)
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
        }
    }

    static void DisplayLoadingIcon(int repetitions)
    {
        Console.ForegroundColor = White;
        string[] loadingSymbols = { "|", "/", "-", "\\" };

        int currentIndex = 0;
        for (int iteration = 0; iteration < repetitions; iteration++)
        {
            Console.Write(" \r" + loadingSymbols[currentIndex++ % loadingSymbols.Length]);
            Thread.Sleep(100);
        }
    }

    static void DisplayErrorMessage(string message) 
    {
        Console.ForegroundColor = ErrorColour;
        Console.Write($"\n\n  {message}");
        Console.ReadKey();
    }
}

