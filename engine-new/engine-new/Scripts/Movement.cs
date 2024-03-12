namespace orion
{
    internal class Movement : Script
    {
        private static int defaultX;
        private static int defaultY;
        private static bool isFirstCall = true;

        public Movement() 
        {
            Name = "movement";
        }

        public static string MovementScript(Player player, ConsoleKeyInfo direction, Scene scene)
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
                    if (newY > 0) 
                        newY--;
                    break;

                case ConsoleKey.DownArrow:
                    if (newY < Console.WindowHeight - 1) 
                        newY++;
                    break;

                case ConsoleKey.LeftArrow:
                    if (newX > 0) 
                        newX--;
                    break;

                case ConsoleKey.RightArrow:
                    if (newX < Console.WindowWidth - 1)
                        newX++;
                    break;

                case ConsoleKey.Tab:
                    player.X = defaultX;
                    player.Y = defaultY;

                    isFirstCall = true;
                    return "exit context";

                default:
                    break;
            }

            String collisionAction = Collider.CheckCollision(player, newX, newY, scene);

            if (collisionAction == "collider")
                return "collision";

            if (collisionAction == "switcher") 
            {
                player.X = defaultX;
                player.Y = defaultY;

                isFirstCall = true;
                return "switcher";
            }

            if (collisionAction == "finisher")
            {
                player.X = defaultX;
                player.Y = defaultY;

                isFirstCall = true;
                return "finisher";
            }

            if (collisionAction == "terminator")
            {
                player.X = defaultX;
                player.Y = defaultY;

                isFirstCall = true;
                return "terminator";
            }

            player.X = newX;
            player.Y = newY;

            Console.SetCursorPosition(previousX, previousY);
            Console.Write(' ');

            Console.SetCursorPosition(player.X, player.Y);
            Console.ForegroundColor = player.Colour;
            Console.Write(player.Symbol);

            return "move";
        }
    }
}
