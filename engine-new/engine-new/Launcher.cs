using System.Reflection.PortableExecutable;

namespace orion;

internal class Launcher
{
    static List<Project> Projects = new();

    static void Main()
    {
        while (true)
        {
            string option = Display.Menu(new List<string>() { "Create", "View", "Load", "Delete", "Settings", "Help", "Exit" }, "  < Orion >  ");

            switch (option)
            {
                case "Create": CreateProject();
                    break;

                case "View": ViewProjects();
                    break;

                case "Load": LoadProject();
                    break;

                case "Delete": DeleteProject();
                    break;
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
                Display.ErrorMessage("project name can not be empty");
                continue;
            }

            if (projectName.Length > 16) 
            {
                Display.ErrorMessage("project name must be less than 16 characters");
                continue;
            }

            if (Projects.Any(project => project.Name == projectName))
            {
                Display.ErrorMessage("a project with that name already exists");
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
        if (Projects.Count == 0) 
        {
            Display.Message("no projects found");
            return;
        }

        Console.Clear();

        Console.ForegroundColor = Display.Gray;
        Console.WriteLine("projects\n\n");

        for (int i = 0; i < Projects.Count; i++)
        {
            Console.ForegroundColor = Display.White;
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
            Display.Message("no projects found");
            return;
        }

        Console.Clear();

        Console.ForegroundColor = Display.Gray;
        Console.WriteLine("projects\n\n");

        List<string?> projects = Projects.Select(project => project.Name).ToList();

        string project = Display.Menu(projects!, "choose a project to load");

        Display.LoadingIcon(6);
    }

    static void DeleteProject()
    {
        if (Projects.Count == 0)
        {
            Display.Message("no projects found");
            return;
        }

        List<string?> projects = Projects.Select(project => project.Name).ToList();

        string projectNameToDelete = Display.Menu(projects!, "choose a project to delete");

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
}

