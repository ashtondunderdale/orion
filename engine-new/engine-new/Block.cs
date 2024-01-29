namespace engine_new;

internal class Block : GameObject
{
    Block(int x, int y, string name) 
    {
        Symbol = Utils.BlockSymbol;
        BasePositionX = x;
        BasePositionY = y;
        Name = name;
    }
}
