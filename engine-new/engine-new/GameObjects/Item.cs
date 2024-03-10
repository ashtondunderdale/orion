namespace orion;

internal class Item : Voxel
{


    public Item(int x, int y) 
    { 
        X = x; Y = y;

        Symbol = 'i';
        Name = "Item";
        Colour = ConsoleColor.Cyan;
    }
}
