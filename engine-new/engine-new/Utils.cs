namespace engine_new;

internal class Utils
{
    public const String PlayerSymbol = "@";
    public const String BlockSymbol = "#";

    public static void CleanConsole() 
    { 
        Console.ReadKey();
        Console.Clear();
    }

    public static void ClearConsole() 
    {
        Console.Clear();
    }

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
