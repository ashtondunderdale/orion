namespace engine_new.Misc;

internal class Utils
{
    public const string ProjectPath = @"C:\Users\adunderdale\ORION\";

    public static void CleanConsole()
    {
        Console.ReadKey();
        Console.Clear();
    }

    public static void ClearConsole() => Console.Clear(); // just go with it

    public static void ShowError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }

    public static void ShowWarning(string warning)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(warning);
        Console.ResetColor();
    }

    public static void ShowMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }
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
