namespace engine_new;

internal class Project
{
    public String Name;
    public List<Scene> Scenes = new();

    Project(String name) 
    {
        Name = name;
    }
}
