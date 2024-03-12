namespace orion;

internal class Help
{
    public static void DisplayHelp() 
    {
        Console.Clear();

        DateTime startDate = new(2024, 3, 8);
        TimeSpan timeSinceStart = DateTime.Now - startDate;
        int daysSinceStart = (int)timeSinceStart.TotalDays;

        Console.WriteLine($"Hello,\n\nIf you are new - this is a terminal game engine project i started {daysSinceStart} days ago. \n\n" +
                          $"If you make anything interesting with it or have any suggestions, please let me know.");

        Console.WriteLine("\n\n\n\n\tguide\n\nstart by creating a project in the main menu (press any key, -> create), then load into that project and create a scene.\n\n" +
                          "from here you will see a scene editor, in there you will be able to add game objects (add object), choose the player.\n\n" +
                          "you will see a pointer on the screen, move using the arrow keys and then press enter to place the object.\n\n" +
                          "go to (return) and select (play).\n\n\n" +
                          "you are now a game developer.");

        Console.ReadKey();
    }
}
