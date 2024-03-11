namespace orion;

internal class Launcher
{
    static List<Project> Projects = new();

    static void Main()
    {
        Console.BackgroundColor = Display.BackgroundColour;

        while (true)
        {
            string option = Display.Menu(new List<string>() { "create", "inspect", "load", "delete", "settings", "help", "exit" }, 
                ". + .. . *  +. <─*─*─ Orion ─*─*─> + *. .. * . . ..     .   . .. + ..   . .    .*  .*  + . .   .       .           .. \n" +
                "     .  . .   .. + *. +. . +* . + .. . + .  .. .      ");

            switch (option)
            {
                case "create": 
                    CreateProject();
                    break;

                case "inspect": 
                    InspectProjects();
                    break;

                case "load": 
                    LoadProject();
                    break;

                case "delete": 
                    DeleteProject();
                    break;

                case "settings":
                    ProjectSettings();
                    break;

                case "help":
                    break;

                case "exit": return;
            }
        }
    }

    static void CreateProject() 
    {
        while (true) 
        {
            Console.Clear();

            Console.ForegroundColor = Display.Gray;
            Console.WriteLine("enter a name for your project\n\n  >");

            Console.ForegroundColor = Display.DarkGray;
            Console.SetCursorPosition(4, 2);
            string? projectName = Console.ReadLine();

            if (string.IsNullOrEmpty(projectName))
            {
                Display.Warning("project name can not be empty");
                continue;
            }

            if (projectName.Length > 16) 
            {
                Display.Warning("project name must be less than 16 characters");
                continue;
            }

            if (Projects.Any(project => project.Name == projectName))
            {
                Display.Warning("a project with that name already exists");
                continue;
            }

            Project project = new(projectName, DateTime.Now);
            Projects.Add(project);

            string loadProject = Display.Menu(new List<string>() { "yes", "no" }, "would you like to load this project now?");

            if (loadProject == "yes") 
            {
                Engine.ProjectMenu(project);
            }

            return;
        }
    }

    static void InspectProjects()
    {
        if (Projects.Count == 0) 
        {
            Display.Warning("no projects found");
            return;
        }

        Console.Clear();

        Console.ForegroundColor = Display.Gray;
        Console.WriteLine("projects\n\n");

        for (int i = 0; i < Projects.Count; i++)
        {
            Console.ForegroundColor = Display.PrimaryColour;
            Console.Write($" {Projects[i].Name,-16}   ");

            Console.ForegroundColor = Display.DarkGray;
            Console.Write($"{Projects[i].CreationDate}\n");
        }

        Console.ReadKey();
    }

    static void LoadProject()
    {
        if (Projects.Count == 0)
        {
            Display.Warning("no projects found");
            return;
        }

        Console.Clear();

        Console.ForegroundColor = Display.Gray;
        Console.WriteLine("projects\n\n");

        List<string?> projects = Projects.Select(project => project.Name).ToList();

        string projectName = Display.Menu(projects!, "choose a project to load");

        if (projectName == "") return;

        Project project = Projects.FirstOrDefault(project => project.Name == projectName)!;

        Engine.ProjectMenu(project);
    }

    static void DeleteProject()
    {
        if (Projects.Count == 0)
        {
            Display.Warning("no projects found");
            return;
        }

        List<string?> projects = Projects.Select(project => project.Name).ToList();

        string projectNameToDelete = Display.Menu(projects!, "choose a project to delete");

        if (projectNameToDelete == "") return;

        string deleteConfirmation = Display.Menu(new List<string>() { "yes", "no" }, $"are you sure you want to delete {projectNameToDelete}?");

        if (deleteConfirmation == "no")
        {
            Display.Message($"project {projectNameToDelete} has not been deleted");
            return;
        }

        Project projectToDelete = Projects.Where(project => project.Name == projectNameToDelete).FirstOrDefault()!;
        Projects.Remove(projectToDelete);

        Display.Message($"project {projectNameToDelete} has been deleted");
    }

    static void ProjectSettings() 
    {
        while (true) 
        {
            Console.Clear();

            string option = Display.Menu(new List<string>() { $"dark mode: {Engine.IsDarkMode}" }, "settings");

            switch (option)
            {
                case "dark mode: True":
                case "dark mode: False":
                    Engine.IsDarkMode = !Engine.IsDarkMode;
                    Display.UpdateTheme();
                    break;

                case "":
                    return;
            }
        }
    }

    static void WriteConstellation()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(20, 20);
        Console.Write("AAAAAAAAAAA");
    }
}