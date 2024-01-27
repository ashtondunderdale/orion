namespace engine_new;

internal class Player : GameObject
{
    public Player(int x, int y, String symbol, string name)
    {
        BasePositionX = x;
        BasePositionY = y;
        Symbol = symbol;
        Name = name;
    }
}
