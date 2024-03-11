using System.Drawing;
using System.Xml.Linq;

namespace orion;

internal class Switcher : Voxel
{
    public Switcher(int x, int y)
    {
        X = x; Y = y;

        Symbol = '\u2794';
        Name = "switcher";
        Colour = ConsoleColor.Yellow;
    }
}
