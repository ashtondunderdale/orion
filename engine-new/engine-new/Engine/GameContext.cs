namespace orion;

internal class GameContext
{
    public static void CreateGameContext(Scene scene)
    {
        if (scene.Name == Engine.ProjectContext!.FinishScene.Name || scene.Name == Engine.ProjectContext!.FailScene.Name) 
        {
            Console.Clear();

            foreach (var obj in scene.Objects)
            {
                Console.SetCursorPosition(obj.X, obj.Y);
                Console.ForegroundColor = obj.Colour;
                Console.Write(obj.Symbol);
            }

            Console.ReadKey();
            return;
        }

        Player? player = scene!.Objects.FirstOrDefault(obj => obj is Player) as Player;

        if (player is null)
        {
            Display.Warning($"Create a player object to play {scene.Name}");
            return;
        }

        // lazy implementation of a game loop - TODO: separate thread
        while (true)
        {
            Console.Clear();

            foreach (var obj in scene.Objects)
            {
                Console.SetCursorPosition(obj.X, obj.Y);
                Console.ForegroundColor = obj.Colour;
                Console.Write(obj.Symbol);
            }

            ConsoleKeyInfo direction;

            if (player is null)
            {
                Display.Warning("Add a player to the scene to play it"); // add error code for reference in help
                return;
            }

            if (player.Scripts.Any(script => script is Movement))
            {
                direction = Console.ReadKey(true);

                string action = Movement.MovementScript(player, direction, scene);

                if (direction.Key == ConsoleKey.Tab)
                {
                    break;
                }

                if (action == "switcher")
                {
                    Scene nextScene = GetNextScene(scene)!;

                    if (nextScene is null)
                    {
                        break;
                    }

                    scene = nextScene;

                    /*
                      reference the new player from the next scene
                      this is because it was using the previous player object from the scene parameter above
                      consequently, the resultant player being used was that of the previous scene - moving invisibly
                    */
                    player = scene.Objects.FirstOrDefault(obj => obj is Player) as Player;
                }
                else if (action == "finisher")
                {
                    scene = Engine.ProjectContext.FinishScene;
                }
                else if (action == "terminator") 
                {
                    scene = Engine.ProjectContext.FailScene;
                }
            }
        }
    }

    static Scene? GetNextScene(Scene scene)
    {
        int nextSceneIndex = Engine.ProjectContext!.SceneSequence.IndexOf(scene.Name!) + 1;

        if (nextSceneIndex < Engine.ProjectContext!.SceneSequence.Count)
        {
            return Engine.ProjectContext!.Scenes.FirstOrDefault(s => s.Name == Engine.ProjectContext.SceneSequence[nextSceneIndex]);
        }
        else
        {
            return null;
        }
    }
}