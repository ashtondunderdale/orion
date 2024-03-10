namespace orion;

internal class SceneEditor
{
    static Scene? SceneContext;

    public static void SceneMenu(Scene scene)
    {
        SceneContext = scene;

        while (true)
        {
            string option = Display.Menu(new List<string>() { "scene editor", "play", "return" }, SceneContext!.Name!);

            switch (option)
            {
                case "scene editor":
                    SceneEditorMenu();
                    break;

                case "play":
                    PlayScene();
                    break;

                case "return": return;
            }
        }
    }

    public static void SceneEditorMenu()
    {
        while (true)
        {
            string option = Display.Menu(new List<string>() { "create object", "remove object", "create preset", "return" }, Engine.ProjectContext!.Name!);

            switch (option)
            {
                case "create object":

                    Voxel voxel = new();

                    while (true) 
                    {
                        string script = CreateObject.Menu();

                        if (script == "") break;

                        switch (script)
                        {
                            case "collider":
                                voxel.Scripts.Add(new Collider());
                                break;

                            case "movement":
                                voxel.Scripts.Add(new Collider());
                                break;

                            case "dynamic colour":
                                voxel.Scripts.Add(new Collider());
                                break;

                            case "dynamic symbol":
                                voxel.Scripts.Add(new DynamicSymbol());
                                break;

                            case "pseudo collider":
                                voxel.Scripts.Add(new PseudoCollider());
                                break;
                        }
                    }

                    Engine.ProjectContext.PresetObjects.Add(voxel);
                    break;

                case "remove object":
                    ModifySceneObjects("delete");
                    break;

                case "create preset":

                    string obj = Display.Menu(Engine.ProjectContext.PresetObjects.Select(voxel => voxel.Name).ToList()!, "choose an object to add");

                    if (SceneContext!.Objects.FirstOrDefault(obj => obj is Player) is Player && obj == "player")
                    {
                        Display.Warning("only one player object per scene is permitted");
                        continue;
                    }

                    ModifySceneObjects(obj);
                    break;

                case "play":
                    PlayScene();
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
            Console.ForegroundColor = Display.PrimaryColour;
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
                var obj = SceneContext.Objects.FirstOrDefault(obj => obj.X == objPointerX && obj.Y == objPointerY);

                Console.SetCursorPosition(objPointerX, objPointerY);
                Console.ForegroundColor = Display.PrimaryColour;
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
                    Player player = new(objPointerX, objPointerY);
                    SceneContext.Objects.Add(player);
                    return;
                }

                if (action == "block") 
                {
                    if (SceneContext.Objects.Any(obj => obj.X == objPointerX && obj.Y == objPointerY)) continue;

                    Block block = new(objPointerX, objPointerY);

                    Console.SetCursorPosition(objPointerX, objPointerY);
                    Console.ForegroundColor = Display.PrimaryColour;
                    Console.Write(block.Symbol);

                    SceneContext.Objects.Add(block);
                }
                else 
                {
                    Voxel obj = SceneContext.Objects.FirstOrDefault(obj => obj.X == objPointerX && obj.Y == objPointerY)!;

                    if (obj is null) continue;                  

                    SceneContext.Objects.Remove(obj);

                    Console.SetCursorPosition(obj.X, obj.Y);
                    Console.Write(' ');
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

    public static void PlayScene()
    {
        Player? player = SceneContext!.Objects.FirstOrDefault(obj => obj is Player) as Player;

        if (player is null) 
        {
            Display.Warning("create a player object to play the level");
            return;
        }

        Console.Clear();

        foreach (var obj in SceneContext.Objects)
        {
            Console.SetCursorPosition(obj.X, obj.Y);
            Console.ForegroundColor = Display.PrimaryColour;
            Console.Write(obj.Symbol);
        }

        while (true)
        {
            ConsoleKeyInfo direction;

            if (player.Scripts.Any(script => script is Movement))
            {
                direction = Console.ReadKey(true);

                Movement.MovementScript(player, direction, SceneContext);

                if (direction.Key == ConsoleKey.Tab)
                {
                    break;
                }
            }
        }
    }
}
