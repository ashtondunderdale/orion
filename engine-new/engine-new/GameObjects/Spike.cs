namespace orion;

internal class Spike : Voxel
{
    public Spike(int x, int y)
    {
        X = x; Y = y;

        Symbol = 'X';
        Name = "spike";
        Colour = ConsoleColor.Red;

        Scripts = new List<Script> { new Terminator() };
    }
}
