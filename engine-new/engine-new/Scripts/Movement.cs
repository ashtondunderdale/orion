namespace orion
{
    internal class Movement : Script
    {
        private static int defaultX;
        private static int defaultY;
        private static bool isFirstCall = true;

        public static void MovementScript(Player player, ConsoleKeyInfo direction, Scene scene)
        {
            if (isFirstCall)
            {
                defaultX = player.X;
                defaultY = player.Y;
                isFirstCall = false;
            }

            int previousX = player.X;
            int previousY = player.Y;

            int newX = player.X;
            int newY = player.Y;

            switch (direction.Key)
            {
                case ConsoleKey.UpArrow:
                    newY--;
                    break;

                case ConsoleKey.DownArrow:
                    newY++;
                    break;

                case ConsoleKey.LeftArrow:
                    newX--;
                    break;

                case ConsoleKey.RightArrow:
                    newX++;
                    break;

                case ConsoleKey.Tab:
                    player.X = defaultX;
                    player.Y = defaultY;

                    isFirstCall = true;
                    return;

                default:
                    break;
            }

            foreach (var obj in scene.Objects)
            {
                if (obj != player && obj.Scripts.Any(script => script is Collider))
                {
                    if (obj.X == newX && obj.Y == newY)
                    {
                        return;
                    }
                }
            }

            player.X = newX;
            player.Y = newY;

            Console.SetCursorPosition(previousX, previousY);
            Console.Write(' ');

            Console.SetCursorPosition(player.X, player.Y);
            Console.Write(player.Symbol);
        }
    }
}
