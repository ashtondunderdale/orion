namespace orion;

internal class SceneEditor
{
    static Scene? SceneContext;

    public static void SceneMenu(Scene scene)
    {
        SceneContext = scene;

        while (true)
        {
            string option = Display.Menu(new List<string>() { "scene editor", "play", "return" }, SceneContext.Name!);

            switch (option)
            {
                case "scene editor":
                    SceneEditorMenu();
                    break;

                case "play":
                    GameContext.CreateGameContext(scene);
                    break;

                case "return": return;
            }
        }
    }

    public static void SceneEditorMenu()
    {
        while (true)
        {
            string option = Display.Menu(new List<string>() { "add object", "remove object", "create preset", "inspect preset", "delete preset", "scene sequence", "finish scene", "return" }, Engine.ProjectContext!.Name!);

            switch (option)
            {
                case "add object":

                    string obj = Display.Menu(Engine.ProjectContext.PresetObjects.Select(voxel => voxel.Name).ToList()!, "choose an object to add");

                    if (SceneContext!.Objects.FirstOrDefault(obj => obj is Player) is Player && obj == "player")
                    {
                        Display.Warning("only one player object per scene is permitted");
                        continue;
                    }

                    ModifySceneObjects(obj);
                    break;

                case "remove object":
                    ModifySceneObjects("delete");
                    break;

                case "create preset":
                    Presets.CreatePresetObject();
                    break;

                case "inspect preset":
                    Presets.InspectPreset();
                    break;

                case "delete preset":
                    Presets.DeletePresetObject();
                    break;

                case "scene sequence":
                    UpdateSceneSequence();
                    break;

                case "finish scene":
                    FinishScene();
                    break;

                case "play":
                    GameContext.CreateGameContext(SceneContext!);
                    break;


                case "return": return;
            }
        }
    }

    static void ModifySceneObjects(string action) 
    {
        Console.Clear();

        foreach (var obj in SceneContext!.Objects)
        {
            Console.SetCursorPosition(obj.X, obj.Y);
            Console.ForegroundColor = obj.Colour;
            Console.Write(obj.Symbol);
        }

        int objPointerX = 0;
        int objPointerY = 0;

        Console.SetCursorPosition(objPointerX, objPointerY);
        Console.ForegroundColor = Display.PointerColour;
        Console.Write("X");

        while (true)
        {
            int previousX = objPointerX;
            int previousY = objPointerY;

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            bool pointerOnObject = SceneContext.Objects.Any(obj => obj.X == objPointerX && obj.Y == objPointerY);

            if (pointerOnObject)
            {
                Voxel obj = SceneContext.Objects.FirstOrDefault(obj => obj.X == objPointerX && obj.Y == objPointerY)!;

                Console.SetCursorPosition(objPointerX, objPointerY);
                Console.ForegroundColor = obj.Colour;
                Console.Write(obj!.Symbol);
            }
            else if (!pointerOnObject)
            {
                Console.SetCursorPosition(previousX, previousY);
                Console.Write(' ');
            }

            if (keyInfo.Key == ConsoleKey.UpArrow && objPointerY > 0)
            {
                objPointerY--;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow && objPointerY < Console.WindowHeight - 1)
            {
                objPointerY++;
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow && objPointerX > 0)
            {
                objPointerX--;
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow && objPointerX < Console.WindowWidth - 1)
            {
                objPointerX++;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (action == "player")
                {
                    if (SceneContext.Objects.Any(obj => obj.X == objPointerX && obj.Y == objPointerY)) continue;

                    Player player = new(objPointerX, objPointerY);
                    SceneContext.Objects.Add(player);
                    return;
                }

                if (action == "block")
                {
                    if (SceneContext.Objects.Any(obj => obj.X == objPointerX && obj.Y == objPointerY)) continue;

                    Block block = new(objPointerX, objPointerY);

                    Console.SetCursorPosition(objPointerX, objPointerY);
                    Console.ForegroundColor = block.Colour;
                    Console.Write(block.Symbol);

                    SceneContext.Objects.Add(block);
                }
                else if (action == "delete")
                {
                    Voxel obj = SceneContext.Objects.FirstOrDefault(obj => obj.X == objPointerX && obj.Y == objPointerY)!;

                    if (obj is null) continue;

                    SceneContext.Objects.Remove(obj);

                    Console.SetCursorPosition(obj.X, obj.Y);
                    Console.Write(' ');
                }
                else // for adding a preset with a dynamic name
                {
                    if (SceneContext.Objects.Any(obj => obj.X == objPointerX && obj.Y == objPointerY)) continue;

                    Voxel matchingObj = Engine.ProjectContext!.PresetObjects.Where(obj => obj.Name == action).FirstOrDefault()!;

                    Voxel obj = new()
                    {
                        Name = matchingObj.Name,
                        Symbol = matchingObj.Symbol,

                        Colour = matchingObj.Colour,

                        Scripts = matchingObj.Scripts,

                        X = objPointerX,
                        Y = objPointerY
                    };

                    Console.SetCursorPosition(objPointerX, objPointerY);
                    Console.ForegroundColor = obj.Colour;
                    Console.Write(obj.Symbol);

                    SceneContext.Objects.Add(obj);
                }
            }
            else if (keyInfo.Key == ConsoleKey.Tab)
            {
                Console.Clear();
                break;
            }

            Console.SetCursorPosition(objPointerX, objPointerY);
            Console.ForegroundColor = Display.PointerColour;
            Console.Write('X');
        }
    }

    static void UpdateSceneSequence()
    {
        List<string> scenes = Engine.ProjectContext!.Scenes.Select(scene => scene.Name).ToList()!;
        List<string> previousSceneSequence = Engine.ProjectContext.SceneSequence; 

        int sceneSequenceLength = Engine.ProjectContext!.Scenes.Count;

        for (int i = 0; i < sceneSequenceLength; i++)
        {
            string scene = Display.Menu(scenes, $"Select a scene to reorder the scene switching sequence\nYour current Scene Sequence is: {string.Join(", ", Engine.ProjectContext!.SceneSequence)}");

            if (scene == "") // for if the user presses tab - resets to previous scene sequence
            {
                Engine.ProjectContext!.SceneSequence = previousSceneSequence;
                return;
            }

            if (i == 0) 
            {
                Engine.ProjectContext!.SceneSequence.Clear();
            }

            Engine.ProjectContext!.SceneSequence.Insert(i, scene);
            scenes.Remove(scene);
        }
    }

    static void FinishScene() 
    {
        Console.Clear();

        string option = Display.Menu(new List<string>() { "view scene", "edit scene" }, "the finish scene will display once your player collides with the last switcher");

        if (option == "view scene") 
        {
            GameContext.CreateGameContext(Engine.ProjectContext!.FinishScene);
        }
        else if (option == "edit scene")
        {
            string obj = Display.Menu(Engine.ProjectContext!.PresetObjects.Select(voxel => voxel.Name).ToList()!, "choose an object to add");

            SceneContext = Engine.ProjectContext.FinishScene;
            ModifySceneObjects(obj);
        }
    }
}
