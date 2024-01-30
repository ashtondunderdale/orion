using System.Drawing;

namespace engine_new.Classes;

internal class Block : GameObject
{
    public Block(int x, int y, string name)
    {
        Symbol = "#";
        Colour = GenerateBlockColour();

        BasePositionX = x;
        BasePositionY = y;
        Name = name;

        Collision = true;
    }

    private static ConsoleColor GenerateBlockColour() 
    {
        Random random = new();
        int colourNumber = random.Next(0, 12);

        if (colourNumber >= 0 && colourNumber < 4)
        {
            return ConsoleColor.White;
        }
        else if (colourNumber >= 4 && colourNumber < 8)
        {
            return ConsoleColor.Gray;
        }
        else
        {
            return ConsoleColor.DarkGray;
        }
    }
}
