namespace orion;

internal class Collider : Script
{
    public Collider() 
    {
        Name = "collider";
    }

    public static string CheckCollision(Player player, int newX, int newY, Scene scene)
    {
        foreach (var obj in scene.Objects)
        {
            if (obj != player && obj.Scripts.Any(script => script is Collider))
            {
                if (obj.X == newX && obj.Y == newY) return "collider";            
            }

            if (obj != player && obj.Name == "switcher") 
            {
                if (obj.X == newX && obj.Y == newY) return "switcher";
            }

            if (obj != player && obj.Name == "finisher")
            {
                if (obj.X == newX && obj.Y == newY) return "finisher";
            }
        }

        return "";
    }
}
