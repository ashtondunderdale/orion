namespace orion;

internal class SceneEditor
{
    static Scene? SceneContext;

    public static void Menu(Scene scene)
    {
        SceneContext = scene;

        while (true)
        {
            string option = Display.Menu(new List<string>() { "create object", "remove object", "return" }, Engine.ProjectContext!.Name!);

            switch (option)
            {
                case "create object":
                    ModifySceneObjects("create");
                    break;

                case "remove object":
                    ModifySceneObjects("delete");
                    break;

                case "play":
                    PlayScene();
                    break;

                case "return": return;
            }
        }
    }

    public static void PlayScene()
    {

    }

    static void ModifySceneObjects(string action) 
    {
        Console.Clear();

        foreach (var obj in SceneContext!.Objects)
        {
            Console.SetCursorPosition(obj.X, obj.Y);
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
                Console.ForegroundColor = Display.White;
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
                if (action == "create")
                {
                    if (SceneContext.Objects.Any(obj => obj.X == objPointerX && obj.Y == objPointerY))
                    {
                        continue;
                    }

                    Block block = new(objPointerX, objPointerY);

                    Console.SetCursorPosition(objPointerX, objPointerY);
                    Console.ForegroundColor = Display.White;
                    Console.Write(block.Symbol);

                    SceneContext.Objects.Add(block);
                }
                else 
                {
                    Voxel obj = SceneContext.Objects.FirstOrDefault(obj => obj.X == objPointerX && obj.Y == objPointerY)!;

                    if (obj is null) 
                    {
                        continue;
                    }

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
}
