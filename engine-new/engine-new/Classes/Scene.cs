namespace engine_new.Classes;

internal class Scene
{
    public string? Name;
    public List<GameObject> GameObjects = new();

    public Scene(string name)
    {
        Name = name;
    }
}
