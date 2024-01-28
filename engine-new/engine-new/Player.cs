namespace engine_new;

internal class Player : GameObject
{
    public Player(int x, int y, string name)
    {
        BasePositionX = x;
        BasePositionY = y;
        Symbol = Utils.PlayerSymbol;
        Name = name;
    }
}
