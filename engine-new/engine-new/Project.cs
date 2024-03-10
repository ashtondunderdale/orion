namespace orion;

internal class Project
{
    public string? Name { get; set; }

    public DateTime CreationDate { get; set; }


    public List<Scene> Scenes = new();

    public List<Voxel> PresetObjects = new() { new Player(0, 0), new Block(0, 0) };

    public Project(string name, DateTime creationDate) 
    {
        Name = name;
        CreationDate = creationDate;
    }
}