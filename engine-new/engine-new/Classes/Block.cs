namespace engine_new.Classes;

internal class Block : GameObject
{
    public Block(int x, int y, string name)
    {
        Symbol = "#";
        Colour = ConsoleColor.White;

        BasePositionX = x;
        BasePositionY = y;
        Name = name;
    }
}
