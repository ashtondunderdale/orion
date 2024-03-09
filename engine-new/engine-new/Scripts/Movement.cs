namespace orion
{
    internal class Movement : Script
    {
        private static int defaultX;
        private static int defaultY;
        private static bool isFirstCall = true;

        public static void MovementScript(Player player, ConsoleKeyInfo direction)
        {
            if (isFirstCall)
            {
                defaultX = player.X;
                defaultY = player.Y;
                isFirstCall = false;
            }

            int previousX = player.X;
            int previousY = player.Y;

            switch (direction.Key)
            {
                case ConsoleKey.UpArrow:
                    player.Y--;
                    break;

                case ConsoleKey.DownArrow:
                    player.Y++;
                    break;

                case ConsoleKey.LeftArrow:
                    player.X--;
                    break;

                case ConsoleKey.RightArrow:
                    player.X++;
                    break;

                case ConsoleKey.Tab:
                    player.X = defaultX;
                    player.Y = defaultY;

                    isFirstCall = true;
                    return;

                default:
                    break;
            }

            Console.SetCursorPosition(previousX, previousY);
            Console.Write(' ');

            Console.SetCursorPosition(player.X, player.Y);
            Console.Write(player.Symbol);
        }
    }
}
