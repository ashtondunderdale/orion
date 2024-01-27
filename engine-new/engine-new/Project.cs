namespace engine_new;

internal class Project
{
    public String Name;
    public List<Scene> Scenes = new();
    public Scene? ActiveScene;

    public Project(String name) 
    {
        Name = name;
    }

    public void Initialise() 
    {
        while (true) 
        {
            Utils.ClearConsole();

            Console.WriteLine($"Project |  {Name}\n");
            
            string? command = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(command))
            {
                Utils.ShowWarning("\nInvalid command.");
                Utils.CleanConsole();
                continue;
            }

            switch (command) 
            {
                case "cr scn":
                    CreateScene();
                    break;

                case "ls scn":
                    ListScenes();
                    Utils.CleanConsole();
                    break;

                case "sl scn":
                    SelectScene();
                    break;

                case "run":
                    RunScene();
                    break;

                default:
                    Utils.ShowWarning("Invalid command.");
                    Utils.CleanConsole();
                    break;
            }
        }
    }

    public void CreateScene() 
    {
        while (true)
        {
            Utils.ClearConsole();

            Utils.ShowMessage("Enter scene name");
            string? sceneName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(sceneName))
            {
                Utils.ShowWarning("\nInvalid scene name.");
                Utils.CleanConsole();
                continue;
            }

            if (SceneExists(sceneName))
            {
                Utils.ShowWarning("\nA scene with this name already exists.");
                Utils.CleanConsole();
                continue;
            }

            Scene scene = new(sceneName);
            Scenes.Add(scene);

            Utils.ShowMessage($"\nCreated scene '{scene.Name}'");
            Utils.CleanConsole();

            break;
        }
    }

    void SelectScene()
    {
        ListScenes();

        if (int.TryParse(Console.ReadLine(), out int sceneIndex) && sceneIndex >= 1 && sceneIndex <= Scenes.Count)
        {
            Scene selectedScene = Scenes[sceneIndex - 1];

            SetActiveScene(selectedScene);

            Utils.ShowMessage($"\nActive scene set '{ActiveScene!.Name}'");
        }
        else
        {
            Utils.ShowError("Invalid input. Scene does not exist.");
        }

        Utils.CleanConsole();
    }

    public void RunScene() 
    { 
    
    }

    bool SceneExists(string sceneName) => Scenes.Any(scene => scene.Name == sceneName);

    void ListScenes()
    {
        int sceneIndex = 1;
        foreach (Scene scene in Scenes)
        {
            Console.WriteLine($"\n{sceneIndex} |  {scene.Name}");
            sceneIndex++;
        }
    }

    void SetActiveScene(Scene scene) => ActiveScene = scene;
}
