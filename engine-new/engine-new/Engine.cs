namespace engine_new;

internal class Engine
{
    List<Project> Projects = new();

    private static void Main() => Initialise();

    private static void Initialise() 
    {
        Console.Write("Initialising Engine..");
        Launcher();
    }

    private static void Launcher() 
    {
        Console.WriteLine("Orion Engine");

        
    }
}