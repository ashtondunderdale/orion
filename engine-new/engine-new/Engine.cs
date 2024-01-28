using System.Reflection.Metadata.Ecma335;

namespace engine_new;

internal class Engine
{
    static List<Project> Projects = new();
    public static Project? ActiveProject;

    private static void Main() => Initialise();

    public static void AddSampleObjects()
    {
        Project project = new("Test Project");
        Projects.Add(project);

        Scene scene = new("Test Scene");
        project.Scenes.Add(scene);
        project.ActiveScene = scene;

        Player player = new(20, 10, "myPlayer");
        scene.GameObjects.Add(player);
    }

    private static void Initialise() 
    {
        Console.Write("Initialising Engine..");
        Console.ReadKey();
        AddSampleObjects(); // here
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
                    Utils.ShowError(Message.InvalidInputWarning);
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
            string? projectName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(projectName)) 
            {
                Utils.ShowWarning(Message.ObjectReferenceCanNotBeEmptyWarning("project"));
                Utils.CleanConsole();
                continue;
            }

            if (ProjectExists(projectName)) 
            {
                Utils.ShowWarning(Message.ObjectAlreadyExistsWarning("project"));
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

        if (NoProjectsExist())
        {
            Utils.ShowWarning(Message.ObjectDoesNotExistsWarning("project"));
            Utils.CleanConsole();
            return;
        }

        ListProjects();
        Console.Write("Enter project index to load.\n");

        if (int.TryParse(Console.ReadLine(), out int chosenProjectIndex) && chosenProjectIndex >= 1 && chosenProjectIndex <= Projects.Count)
        {
            Project chosenProject = Projects[chosenProjectIndex - 1];
            SetActiveProject(chosenProject);

            Utils.ShowMessage($"\nLoading {ActiveProject!.Name}");

            chosenProject.Initialise();
        }
        else Utils.ShowError(Message.ObjectDoesNotExistsWarning("project"));

        Utils.CleanConsole();
    }

    private static void DeleteProject()
    {
        Utils.ClearConsole();

        if (NoProjectsExist())
        {
            Utils.ShowWarning(Message.ObjectDoesNotExistsWarning("project"));
            Utils.CleanConsole();
            return;
        }

        ListProjects();
        Console.Write("Enter project index to delete\n");

        if (int.TryParse(Console.ReadLine(), out int chosenProjectIndex) && chosenProjectIndex >= 1 && chosenProjectIndex <= Projects.Count)
        {
            Project chosenProject = Projects[chosenProjectIndex - 1];
            Projects.Remove(chosenProject);
            Utils.ShowMessage($"\nDeleted Project '{chosenProject.Name}'");
        }
        else Utils.ShowWarning(Message.ObjectDoesNotExistsWarning("project"));


        Utils.CleanConsole();
    }

    static bool NoProjectsExist() => Projects.Count == 0;

    static bool ProjectExists(string projectName) => Projects.Any(project => project.Name == projectName);

    static void ListProjects() 
    {
        int projectIndex = 1;
        foreach (Project project in Projects)
        {
            Console.WriteLine($"{projectIndex} |  {project.Name}\n");
            projectIndex++;
        }
    }

    static void SetActiveProject(Project project) => ActiveProject = project;
}