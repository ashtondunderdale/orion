namespace orion;

internal class Player : Voxel 
{

    public Player(int x, int y) 
    { 
        X = x; Y = y;

        Symbol = '0';
        Name = "player";
        Colour = ConsoleColor.Cyan;

        Scripts = new List<Script>() { new Movement() };
    }
}
