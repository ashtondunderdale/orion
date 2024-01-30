using Newtonsoft.Json;
using static System.Formats.Asn1.AsnWriter;

namespace engine_new;

internal class Project
{
    public String Name;
    public List<Scene> Scenes = new();
    public Scene? ActiveScene;

    public string ProjectJsonPath => Utils.ProjectPath + Name;

    public Project(String name)
    {
        Name = name;
    }

    public void Initialise()
    {
        LoadProject();

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

                case "rdr":
                    RenderScene();
                    Utils.CleanConsole();
                    break;

                case "ply":
                    PlayScene();
                    break;

                case "rtn":
                    Utils.ShowMessage("\nReturning to Launcher.");
                    return;

                case "sav": // add auto saving
                    SaveProject();
                    Utils.ShowMessage($"\nProject saved at: {Utils.ProjectPath}");
                    break;

                case "gr scn":
                    GenerateRandomScene();
                    break;

                case "gp scn":
                    GenerateProceduralScene();
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
            if (ActiveScene is null)
            {
                Utils.ShowWarning("\nActive scene must not be null to add objects.");
                Utils.CleanConsole();
                return;
            }

            Console.WriteLine("\nEnter the type of game object to create.\n\n");
            Console.WriteLine("1 |  Player\n");
            Console.WriteLine("2 |  Block\n");

            string? objectInput = Console.ReadLine();

            if (int.TryParse(objectInput, out int objectType) && objectType == 1)
            {

                if (ActiveScene?.GameObjects.OfType<Player>().Any() == true)
                {
                    Utils.ShowWarning("\nPlayer already exists in this space.");
                    Utils.CleanConsole();
                    return;
                }

                Console.WriteLine("\nEnter the X coordinate:");
                int x = int.Parse(Console.ReadLine());

                Console.WriteLine("\nEnter the Y coordinate:");
                int y = int.Parse(Console.ReadLine());

                Console.WriteLine("\nEnter the name:");
                string name = Console.ReadLine();

                Player player = new(x, y, name);
                ActiveScene!.GameObjects.Add(player);

                Utils.ShowMessage($"\nCreated object '{player.Name}'.");

                Utils.CleanConsole();
                break;
            }
            else if (int.TryParse(objectInput, out objectType) && objectType == 2)
            {
                Console.WriteLine("\nEnter the X coordinate:");
                int x = int.Parse(Console.ReadLine());

                Console.WriteLine("\nEnter the Y coordinate:");
                int y = int.Parse(Console.ReadLine());

                Console.WriteLine("\nEnter the name:");
                string name = Console.ReadLine();

                Block block = new(x, y, name);
                ActiveScene!.GameObjects.Add(block);

                Utils.ShowMessage($"\nCreated object '{block.Name}'.");

                Utils.CleanConsole();
                break;
            }
            else
            {
                Utils.ShowError(Message.InvalidInputWarning);
                Utils.CleanConsole();
                return;
            }
        }
    }

    public Scene CreateScene()
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

            return scene;
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
            Utils.ShowWarning("\nSelect an Active Scene to render / play.");
            Utils.CleanConsole();
            return;
        }

        Utils.ClearConsole();

        foreach (GameObject obj in ActiveScene.GameObjects)
        {
            Console.SetCursorPosition(obj.BasePositionX, obj.BasePositionY);

            Console.ForegroundColor = obj.Colour;
            Console.Write(obj.Symbol);
            Console.ResetColor();
        }
    }

    public void PlayScene()
    {
        RenderScene();

        if (ActiveScene is null) return;

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

                    player?.Move(action.Key, ActiveScene!.GameObjects);

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

    public void SaveProject()
    {
        if (!Directory.Exists(ProjectJsonPath)) Directory.CreateDirectory(ProjectJsonPath);

        using StreamWriter sw = File.CreateText(ProjectJsonPath + $"\\{Name}.json");
        {
            JsonSerializerSettings settings = new()
            {
                Formatting = Formatting.Indented
            };

            var projectData = new
            {
                ProjectName = Name,
                Greeting = $"Hello! This is your saved project file for {Name}",
                Scenes = Scenes.Select(scene => new
                {
                    SceneName = scene.Name,
                    GameObjects = scene.GameObjects.Select(obj => JsonConvert.SerializeObject(obj, settings)).ToList()
                }).ToList()
            };

            string projectJson = JsonConvert.SerializeObject(projectData, settings);
            sw.Write(projectJson);
        }
    }

    public void LoadProject()
    {
        string filePath = Path.Combine(ProjectJsonPath, $"{Name}.json");

        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);

            JsonSerializerSettings settings = new()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            ProjectData? loadedProjectData = JsonConvert.DeserializeObject<ProjectData>(jsonContent, settings);

            ClearProject();

            foreach (var loadedSceneData in loadedProjectData!.Scenes)
            {
                Scene scene = new(loadedSceneData.SceneName);

                foreach (var loadedObjectData in loadedSceneData.GameObjects)
                {
                    GameObject? obj;

                    if (loadedObjectData.Contains("\"Name\": \"player\"")) obj = JsonConvert.DeserializeObject<Player>(loadedObjectData, settings);
                    else obj = JsonConvert.DeserializeObject<GameObject>(loadedObjectData, settings);
                    
                    scene.GameObjects.Add(obj!);
                }
                Scenes.Add(scene);
            }
        }
    }

    public void GenerateRandomScene()
    {
        Scene scene = CreateScene();

        for (int y = 0; y < Console.WindowHeight; y++)
        {
            for (int x = 0; x < Console.WindowWidth; x++)
            {
                Random random = new();
                int blockPercentage = 10;

                if (random.Next(100) < blockPercentage)
                {
                    Console.SetCursorPosition(x, y);
                    Block block = new(x, y, "block");

                    scene!.GameObjects.Add(block);
                }
            }
        }
    }

    public void GenerateProceduralScene()
    {
        Random random = new();
        Scene scene = CreateScene();

        int w = 20;
        int h = 10;

        int s = 4;

        int x = random.Next(0, Console.WindowWidth - w);
        int y = random.Next(0, Console.WindowHeight - w);

        int e = random.Next(0, 4);

        int playerX = random.Next(x + 1, x + w - 1);
        int playerY = random.Next(y + 1, y + h - 1);

        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if ((i == 0 || i == h - 1 || j == 0 || j == w - 1) &&
                    !(
                        (e == 0 && i == 0 && j >= (w - s) / 2 && j < (w + s) / 2) ||
                        (e == 1 && j == w - 1 && i >= (h - s) / 2 && i < (h + s) / 2) ||
                        (e == 2 && i == h - 1 && j >= (w - s) / 2 && j < (w + s) / 2) ||
                        (e == 3 && j == 0 && i >= (h - s) / 2 && i < (h + s) / 2)
                    )
                )
                {
                    Block block = new(x + j, y + i, "block");

                    if (IsWithinTerminalBounds(block.BasePositionX, block.BasePositionY))
                    {
                        scene!.GameObjects.Add(block);
                    }
                }
            }
        }
        Player player = new(playerX, playerY, "player");
        scene!.GameObjects.Add(player);
    }


    public void GenerateProceduralSquares()
    {
        Scene scene = CreateScene();

        int blockWidth = 20;
        int blockHeight = 10;
        Random random = new();

        for (int i = 0; i < 10; i++)
        {
            int x = random.Next(0, Console.WindowWidth - blockWidth);
            int y = random.Next(0, Console.WindowHeight - blockHeight);

            if (IsWithinTerminalBounds(x, y))
            {
                for (int j = 0; j < blockWidth; j++)
                {
                    Block block = new(x + j, y, "block");

                    if (IsWithinTerminalBounds(block.BasePositionX, block.BasePositionY)) scene!.GameObjects.Add(block);  
                }

                for (int k = 0; k < blockHeight; k++)
                {
                    Block block = new(x, y + k, "block");

                    if (IsWithinTerminalBounds(block.BasePositionX, block.BasePositionY)) scene!.GameObjects.Add(block);
                }

                for (int l = 0; l < blockWidth; l++)
                {
                    Block block = new(x + l, y + blockHeight - 1, "block");

                    if (IsWithinTerminalBounds(block.BasePositionX, block.BasePositionY)) scene!.GameObjects.Add(block);
                }

                for (int m = 0; m < blockHeight; m++)
                {
                    Block block = new(x + blockWidth - 1, y + m, "block");

                    if (IsWithinTerminalBounds(block.BasePositionX, block.BasePositionY)) scene!.GameObjects.Add(block);
                }
            }
        }
    }


    private static bool IsWithinTerminalBounds(int x, int y) => x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight;
    
    private void ClearProject() => Scenes.Clear();
    
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

public class ProjectData
{
    public string ProjectName { get; set; }
    public List<SceneData> Scenes { get; set; }
}

public class SceneData
{
    public string SceneName { get; set; }
    public List<string> GameObjects { get; set; }
}
