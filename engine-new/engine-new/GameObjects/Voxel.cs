namespace orion;

internal class Voxel
{
    public string? Type { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public Voxel(int x, int y) 
    {
        X = x; Y = y;
    }
}
