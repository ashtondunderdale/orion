namespace orion;

internal class Scene
{
    public string? Name { get; set; }

    public List<Voxel> Objects = new();

    public Scene(string name) 
    {
        Name = name;
    }
}
