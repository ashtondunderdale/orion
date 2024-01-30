using System.Drawing;

namespace engine_new.Classes;

internal class Floor : GameObject
{
    public Floor(int x, int y, string name)
    {
        Symbol = "\"";
        Colour = ConsoleColor.White;

        BasePositionX = x;
        BasePositionY = y;
        Name = name;

        Collision = false;
    }
}
