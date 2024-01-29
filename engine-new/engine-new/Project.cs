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

            string mainCommand = command.Contains(' ') ? $"{command.Split(' ')[0]} {command.Split(' ')[1]}" : command;

            switch (mainCommand) 
            {
                case "cr obj":
                    CreateObject(command);
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

                case "rdr":
                    RenderScene();
                    Utils.CleanConsole();
                    break;

                case "ply":
                    PlayScene();
                    break;

                default:
                    Utils.ShowWarning(Message.CommandDoesNotExistWarning);
                    Utils.CleanConsole();
                    break;
            }
        }
    }

    public void CreateObject(string command)
    {
        while (true)
        {
            if (ActiveScene is null) 
            {
                Utils.ShowWarning("\nActive scene must not be null to add objects.");
                Utils.CleanConsole();
                return;
            }

            Console.WriteLine("\nEnter the type of game object to create.\n\n");
            Console.WriteLine("1 |  Player\n");

            if (int.TryParse(Console.ReadLine(), out int objectType) && objectType == 1)
            {

                if (ActiveScene?.GameObjects.OfType<Player>().Any() == true)
                {
                    Utils.ShowWarning("\nPlayer already exists in this space.");
                    Utils.CleanConsole();
                    return;
                }

                Console.WriteLine("Enter the X coordinate:");
                int x = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the Y coordinate:");
                int y = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the name:");
                string name = Console.ReadLine();

                Player player = new(x, y, name);
                ActiveScene!.GameObjects.Add(player);

                Utils.ShowMessage($"\nCreated object '{player.Name}'.");

                Utils.CleanConsole();
                break;
            }
            else if (int.TryParse(Console.ReadLine(), out objectType) && objectType == 1) 
            {
                Console.WriteLine("Enter the X coordinate:");
                int x = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the Y coordinate:");
                int y = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the name:");
                string name = Console.ReadLine();

                Player player = new(x, y, name);
                ActiveScene!.GameObjects.Add(player);

                Utils.ShowMessage($"\nCreated object '{player.Name}'.");

                Utils.CleanConsole();
                break;
            }

            else
            {
                Utils.ShowError(Message.InvalidInputWarning);
                Utils.CleanConsole();
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
    }

    public void PlayScene() 
    {
        RenderScene();

        Player player = ActiveScene!.GameObjects.OfType<Player>().FirstOrDefault()!;

        while (true) 
        { 
            var action = Console.ReadKey();

            switch (action.Key) 
            {
                case ConsoleKey.UpArrow:     
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:


                    player?.Move(action.Key);
                    break;

                case ConsoleKey.Tab:
                    Utils.ClearConsole();
                    player?.ResetPlayerPosition();
                    return;

                default: // fix so that if user types any characters - they will not show up.
                    continue;
            }
        }
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
