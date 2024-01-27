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
            Utils.ShowMessage("\nEnter project name");
            string? projectName = Console.ReadLine();

            if (string.IsNullOrEmpty(projectName)) 
            {
                Utils.ShowWarning("Invalid project name.");
                Utils.CleanConsole();
                continue;
            }

            if (ProjectExists(projectName)) 
            {
                Utils.ShowWarning("A project with this name already exists.");
                Utils.CleanConsole();
                continue;
            }

            Project project = new(projectName);
            Projects.Add(project);
        }

    }

    private static void LoadProject()
    {

    }

    private static void DeleteProject()
    {

    }

    static bool ProjectExists(string projectName) => Projects.Any(project => project.Name == projectName);  
}