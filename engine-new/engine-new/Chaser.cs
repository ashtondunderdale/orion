using System.Drawing;
using System.Xml.Linq;

namespace engine_new;

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

    public void ChasePlayer() 
    { 
        
    }
}
