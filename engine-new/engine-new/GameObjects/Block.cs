namespace orion;

internal class Block : Voxel
{


    public Block(int x, int y)
    {
        X = x; Y = y;

        Symbol = '#';
        Name = "block";
        Colour = ConsoleColor.DarkGray;

        Scripts = new List<Script> { new Collider() };
    }
}