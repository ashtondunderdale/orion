namespace engine_new;

internal class Player : GameObject
{
    public int ActiveX;
    public int ActiveY;

    public Player(int x, int y, string name)
    {
        BasePositionX = x;
        BasePositionY = y;
        ActiveX = BasePositionX;
        ActiveY = BasePositionY;

        Symbol = Utils.PlayerSymbol;
        Name = name;
    }

    public void Move(ConsoleKey key) 
    {
        int previousX = ActiveX;
        int previousY = ActiveY;    

        switch (key) 
        {
            case ConsoleKey.UpArrow:
                ActiveY -= 1;
                break;

            case ConsoleKey.DownArrow:
                ActiveY += 1;

                break;

            case ConsoleKey.LeftArrow:
                ActiveX -= 1;

                break;

            case ConsoleKey.RightArrow:
                ActiveX += 1;

                break;
        }

        Console.SetCursorPosition(previousX, previousY);
        Console.Write(' ');
        Console.SetCursorPosition(ActiveX, ActiveY);
        Console.Write(Symbol);
    }

    public void ResetPlayerPosition() 
    {
        ActiveX = BasePositionX;
        ActiveY = BasePositionY;
    }
}
