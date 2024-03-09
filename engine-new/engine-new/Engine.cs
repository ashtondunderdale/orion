using static System.Formats.Asn1.AsnWriter;

namespace orion;

class Engine
{
    static Project? ProjectContext;
    static Scene? SceneContext;

    public static void CreateProjectContext(Project project)
    {
        Display.LoadingIcon(4);
        Console.Clear();

        ProjectContext = project;
        ProjectMenu();
    }

    static void ProjectMenu() 
    {
        while (true) 
        {
            string commandChoice = Display.Menu(new List<string>() { "create scene", "load scene", "inspect scene" }, ProjectContext!.Name!);

            switch (commandChoice)
            {
                case "create scene":
                    CreateScene();
                    break;

                case "load scene":
                    LoadScene();
                    break;

                case "inspect scene":
                    InspectScene();
                    break;
            }
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

            if (ProjectContext!.Scenes.Any(scene => scene.Name == sceneName))
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
        if (ProjectContext!.Scenes.Count == 0)
        {
            Display.Warning("no scenes found");
            return;
        }

        Console.Clear();

        Console.ForegroundColor = Display.Gray;
        Console.WriteLine("scenes\n\n");

        List<string?> scenes = ProjectContext!.Scenes.Select(scene => scene.Name).ToList();

        string sceneName = Display.Menu(scenes!, "choose a scene to load");
        Scene scene = ProjectContext.Scenes.FirstOrDefault(scene => scene.Name == sceneName)!;

        CreateSceneContext(scene);
    }

    static void InspectScene()
    {

        if (ProjectContext!.Scenes.Count == 0)
        {
            Display.Warning("no scenes found");
            return;
        }

        Console.Clear();

        Console.ForegroundColor = Display.Gray;
        Console.WriteLine("scenes\n\n");

        List<string?> scenes = ProjectContext!.Scenes.Select(scene => scene.Name).ToList();

        string sceneName = Display.Menu(scenes!, "choose a scene to inspect");
        Scene scene = ProjectContext.Scenes.FirstOrDefault(scene => scene.Name == sceneName)!;

        Console.Clear();
        Console.Write(scene.Name);

        Console.ReadKey();

        return;

    }

    public static void CreateSceneContext(Scene scene)
    {
        SceneContext = scene;

        Console.WriteLine(scene.Name);
        Console.ReadKey();
    }
}