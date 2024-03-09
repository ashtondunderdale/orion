namespace orion;

internal class SceneEditor
{
    static Scene? SceneContext;

    public static void Menu(Scene scene)
    {
        SceneContext = scene;

        while (true)
        {
            string option = Display.Menu(new List<string>() { "create object", "return" }, Engine.ProjectContext!.Name!);

            switch (option)
            {
                case "create object":
                    CreateObject();
                    break;

                case "remove object":
                    RemoveObject();
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

    static void CreateObject()
    {
        Console.Clear();

        foreach (var obj in SceneContext!.Objects)
        {
            Console.SetCursorPosition(obj.X, obj.Y);
            Console.Write(obj.Symbol);
        }

        int objPointerX = 0;
        int objPointerY = 0;

        Console.Write("X");

        while (true)
        {
            int previousX = objPointerX;
            int previousY = objPointerY;

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            Console.SetCursorPosition(previousX, previousY);
            Console.Write(' ');

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
                Voxel voxel = new(objPointerX, objPointerY);
                SceneContext.Objects.Add(voxel);
            }
            else if (keyInfo.Key == ConsoleKey.Tab)
            {
                return;
            }

            Console.SetCursorPosition(objPointerX, objPointerY);
            Console.Write('X');
        }
    }

    static void RemoveObject()
    {

    }
}
