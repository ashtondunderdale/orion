namespace engine_new;

internal class Player : GameObject
{
    Player(int x, int y, String symbol)
    {
        BasePositionX = x;
        BasePositionY = y;
        Symbol = symbol;
    }
}
