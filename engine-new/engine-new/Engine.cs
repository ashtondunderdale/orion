using System.Reflection.Metadata.Ecma335;

namespace engine_new;

internal class Engine
{
    static List<Project> Projects = new();
    static Project? ActiveProject;

    private static void Main() => Initialise();

    private static void Initialise() 
    {
        Console.Write("Initialising Engine..");
        Console.ReadKey();
        Launcher();
    }

    private static void Launcher() 
    {
        while (true)
        {
            Console.WriteLine("Orion Engine\n\n");

            Console.WriteLine("1 |  Create Project");
            Console.WriteLine("2 |  Load Project");
            Console.WriteLine("3 |  Delete Project\n");

            Console.WriteLine("4 |  Exit Launcher\n\n");

            String? launcherInput = Console.ReadLine();

            switch (launcherInput) 
            {
                case "1":
                    CreateProject();
                    break;

                case "2":
                    LoadProject();
                    break;

                case "3":
                    DeleteProject();
                    break;

                case "4":
                    return;

                default:
                    Utils.ShowError("Invalid Option");
                    Utils.CleanConsole();
                    continue;
            }
        }
    }

    private static void CreateProject() 
    {
        while (true) 
        {
            Utils.ClearConsole();

            Utils.ShowMessage("Enter project name");
            string? projectName = Console.ReadLine();

            if (string.IsNullOrEmpty(projectName)) 
            {
                Utils.ShowWarning("\nInvalid project name.");
                Utils.CleanConsole();
                continue;
            }

            if (ProjectExists(projectName)) 
            {
                Utils.ShowWarning("\nA project with this name already exists.");
                Utils.CleanConsole();
                continue;
            }

            Project project = new(projectName);
            Projects.Add(project);

            Utils.ShowMessage($"\nCreated Project '{project.Name}'");
            Utils.CleanConsole();

            break;
        }

    }

    private static void LoadProject()
    {
        Utils.ClearConsole();

        if (Projects.Count == 0)
        {
            Utils.ShowWarning("There are no projects to display.");
            Utils.CleanConsole();
            return;
        }

        int projectIndex = 1;
        foreach (Project project in Projects)
        {
            Console.WriteLine($"{projectIndex} |  {project.Name}\n");
            projectIndex++;
        }

        Console.Write("Enter the number of the project to load\n");
        if (int.TryParse(Console.ReadLine(), out int chosenProjectIndex) && chosenProjectIndex >= 1 && chosenProjectIndex <= Projects.Count)
        {
            Project chosenProject = Projects[chosenProjectIndex - 1];
            ActiveProject = chosenProject;
            Utils.ShowMessage($"\nLoading {chosenProject.Name}");
        }
        else
        {
            Utils.ShowError("\nInvalid input. Project does not exist.");
        }

        Utils.CleanConsole();
    }

    private static void DeleteProject()
    {

    }

    static bool ProjectExists(string projectName) => Projects.Any(project => project.Name == projectName);  
}