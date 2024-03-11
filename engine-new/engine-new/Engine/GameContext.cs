namespace orion;

internal class GameContext
{
    public static void CreateGameContext(Scene scene) 
    {
        Player? player = scene!.Objects.FirstOrDefault(obj => obj is Player) as Player;

        if (player is null)
        {
            Display.Warning("create a player object to play the level");
            return;
        }

        Console.Clear();

        foreach (var obj in scene.Objects)
        {
            Console.SetCursorPosition(obj.X, obj.Y);
            Console.ForegroundColor = obj.Colour;
            Console.Write(obj.Symbol);
        }

        while (true)
        {
            ConsoleKeyInfo direction;

            if (player.Scripts.Any(script => script is Movement))
            {
                direction = Console.ReadKey(true);

                Movement.MovementScript(player, direction, scene);

                if (direction.Key == ConsoleKey.Tab)
                {
                    break;
                }
            }
        }
    }
}
