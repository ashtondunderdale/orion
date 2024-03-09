namespace orion;

class Engine
{
    public static Project? ProjectContext;

    public static void ProjectMenu(Project project)
    {
        ProjectContext = project;

        while (true) 
        {
            string option = Display.Menu(new List<string>() { "create", "load", "inspect", "play", "return" }, ProjectContext!.Name!);

            switch (option)
            {
                case "create":
                    CreateScene();
                    break;

                case "load":
                    LoadScene();
                    break;

                case "inspect":
                    InspectScene();
                    break;

                case "play":
                    PlayProject();
                    break;

                case "return": return;
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

        if (sceneName == "") return;

        Scene scene = ProjectContext.Scenes.FirstOrDefault(scene => scene.Name == sceneName)!;

        SceneEditor.SceneMenu(scene!);
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

        if (sceneName == "") return;

        Scene scene = ProjectContext.Scenes.FirstOrDefault(scene => scene.Name == sceneName)!;

        Console.Clear();

        Console.Write($"{scene.Name}\n");
        Console.Write($"Object Count: {scene.Objects.Count}");         

        Console.ReadKey();

        return;
    }

    static void PlayProject() 
    {

    }
}