namespace orion;

internal class Project
{
    public string? Name { get; set; }

    public DateTime CreationDate { get; set; }


    public List<Scene> Scenes = new();

    public Project(string name, DateTime creationDate) 
    {
        Name = name;
        CreationDate = creationDate;
    }
}
