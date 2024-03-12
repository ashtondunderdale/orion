using System.Drawing;
using System.Xml.Linq;

namespace orion;

internal class Finisher : Voxel
{
    public Finisher(int x, int y)
    {
        X = x; Y = y;

        Symbol = '&';
        Name = "finisher";
        Colour = ConsoleColor.Green;
    }
}
