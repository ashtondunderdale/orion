namespace orion;

internal class Voxel
{
    public string? Name { get; set; }

    public char Symbol { get; set; }

    public ConsoleColor Colour { get; set; }

    public int X { get; set; }

    public int Y { get; set; }


    public List<Script> Scripts = new();
}