using System;

namespace engine_new
{
    internal class Chaser : GameObject
    {
        public int ActiveX;
        public int ActiveY;

        public Chaser(int x, int y, string name)
        {
            BasePositionX = x;
            BasePositionY = y;
            ActiveX = BasePositionX;
            ActiveY = BasePositionY;

            Colour = ConsoleColor.Red;
            Symbol = "0";
            Name = name;
        }

        public void ChasePlayer(Player player)
        {
            int directionX = Math.Sign(player.ActiveX - ActiveX);
            int directionY = Math.Sign(player.ActiveY - ActiveY);

            MoveChaser(directionX, directionY);
        }

        private void MoveChaser(int directionX, int directionY)
        {
            Console.SetCursorPosition(ActiveX, ActiveY);
            Console.Write(' ');

            ActiveX += directionX;
            ActiveY += directionY;

            Console.SetCursorPosition(ActiveX, ActiveY);
            Console.ForegroundColor = Colour;
            Console.Write(Symbol);
            Console.ResetColor();
        }
    }
}
