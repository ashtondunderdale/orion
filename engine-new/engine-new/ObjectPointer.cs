using System.Drawing;

namespace engine_new;

internal class ObjectPointer : GameObject
{
    public int ActiveX;
    public int ActiveY;

    public ObjectPointer(int x, int y)
    {
        BasePositionX = x;
        BasePositionY = y;
        ActiveX = BasePositionX;
        ActiveY = BasePositionY;

        Colour = ConsoleColor.White;
        Symbol = "X";
    }

    public void Move(ConsoleKey key)
    {
        int targetX = ActiveX;
        int targetY = ActiveY;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                targetY -= 1;
                break;

            case ConsoleKey.DownArrow:
                targetY += 1;
                break;

            case ConsoleKey.LeftArrow:
                targetX -= 1;
                break;

            case ConsoleKey.RightArrow:
                targetX += 1;
                break;
        }

        Console.SetCursorPosition(ActiveX, ActiveY);
        Console.Write(' ');

        ActiveX = targetX;
        ActiveY = targetY;

        Console.SetCursorPosition(ActiveX, ActiveY);

        Console.ForegroundColor = Colour;
        Console.Write(Symbol);
        Console.ResetColor();
    }
}
