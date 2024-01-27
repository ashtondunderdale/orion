namespace engine_new;

internal class Scene
{
    public String? Name;
    public List<GameObject> GameObjects = new();

    public Scene(string name) 
    {
        Name = name;
    }
}
