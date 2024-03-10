namespace orion;

internal class CreateObject
{
    public static string Menu() 
    {
        string option = Display.Menu(new List<string>() { "movement", "collider", "dynamic colour", "dynamic symbol", "pseudo collider" }, "select scripts for your custom object");
            
        return option;
    }
}
