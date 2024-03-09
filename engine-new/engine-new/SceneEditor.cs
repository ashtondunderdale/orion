namespace orion;

internal class SceneEditor
{
    public static void Menu()
    {
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
        foreach (var obj in Engine.SceneContext!.Objects)
        {
            Console.SetCursorPosition(obj.X, obj.Y);
            Console.Write(obj);
        }
    }

    static void CreateObject()
    {
        Console.Clear();


    }

    static void RemoveObject()
    {

    }
}
