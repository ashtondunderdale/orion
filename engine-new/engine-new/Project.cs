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
                Utils.ShowWarning(Message.CommandDoesNotExistWarning);
                Utils.CleanConsole();
                continue;
            }

            switch (command) 
            {
                case "cr obj":
                    CreateObject();
                    break;

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

                case "render":
                    RenderScene();
                    break;

                default:
                    Utils.ShowWarning(Message.CommandDoesNotExistWarning);
                    Utils.CleanConsole();
                    break;
            }
        }
    }

    public void CreateObject() 
    {
        while (true) 
        {
            Console.WriteLine("Enter the type of game object to create");
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
                Utils.ShowWarning(Message.ObjectReferenceCanNotBeEmptyWarning("scene"));
                Utils.CleanConsole();
                continue;
            }

            if (SceneExists(sceneName))
            {
                Utils.ShowWarning(Message.ObjectAlreadyExistsWarning("scene"));
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
        if (NoScenesExist()) 
        {
            Utils.ShowWarning(Message.ObjectDoesNotExistsWarning("scene"));
            Utils.CleanConsole();
            return;
        }

        ListScenes();

        if (int.TryParse(Console.ReadLine(), out int sceneIndex) && sceneIndex >= 1 && sceneIndex <= Scenes.Count)
        {
            Scene selectedScene = Scenes[sceneIndex - 1];

            SetActiveScene(selectedScene);

            Utils.ShowMessage($"\nActive scene set '{ActiveScene!.Name}'");
        }
        else Utils.ShowError(Message.ObjectDoesNotExistsWarning("scene"));      

        Utils.CleanConsole();
    }

    public void RenderScene() 
    {
        if (ActiveScene is null) 
        {
            Utils.ShowWarning("Select an Active Scene to run.");
            Utils.CleanConsole();
            return;
        }

        Utils.ClearConsole();

        foreach (GameObject obj in ActiveScene.GameObjects) 
        {
            Console.SetCursorPosition(obj.BasePositionX, obj.BasePositionY);
            Console.Write(obj.Symbol);
        }

        Utils.CleanConsole();
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

    bool NoScenesExist() => Scenes.Count == 0;
}
