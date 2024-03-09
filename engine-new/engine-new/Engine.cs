namespace orion;

class Engine
{
    static Project? ProjectContext;

    public static void CreateProjectContext(Project project)
    {
        Display.LoadingIcon(4);
        Console.Clear();

        ProjectContext = project;
        ProjectMenu();
    }

    static void ProjectMenu() 
    {
        string commandChoice = Display.Menu(new List<string>() { "create scene", "load scene", "inspect scene" }, ProjectContext!.Name!);

        switch (commandChoice)
        {
            case "create scene": CreateScene();
                break;

            case "load scene": LoadScene();
                break;

            case "inspect scene": InspectScene();
                break;
        }
    }

    static void CreateScene() 
    {
        while (true) 
        {
            Console.Clear();

            Console.ForegroundColor = Display.Gray;
            Console.WriteLine("enter a name for your scene\n\n  >");

            Console.ForegroundColor = Display.DarkGray;
            Console.SetCursorPosition(4, 2);
            string? sceneName = Console.ReadLine();

            if (string.IsNullOrEmpty(sceneName))
            {
                Display.Warning("scene name can not be empty");
                continue;
            }

            if (ProjectContext!.Scenes.Any(project => project.Name == sceneName))
            {
                Display.Warning("a scene with that name already exists");
                continue;
            }
            Scene scene = new(sceneName);
            ProjectContext.Scenes.Add(scene);

            return;
        }
    }

    static void LoadScene()
    {

    }

    static void InspectScene()
    {

    }
}