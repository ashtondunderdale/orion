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

    public void Move(ConsoleKey key, List<GameObject> activeProjectGameObjects)
    {
        int targetX = ActiveX;
        int targetY = ActiveY;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                targetY -= 1;
                break;

            case ConsoleKey.DownArrow:
                targetY += 1;
                break;

            case ConsoleKey.LeftArrow:
                targetX -= 1;
                break;

            case ConsoleKey.RightArrow:
                targetX += 1;
                break;
        }

        if (!IsBlockCollision(activeProjectGameObjects, targetX, targetY))
        {
            Console.SetCursorPosition(ActiveX, ActiveY);
            Console.Write(' ');

            ActiveX = targetX;
            ActiveY = targetY;

            Console.SetCursorPosition(ActiveX, ActiveY);
            Console.Write(Symbol);
        }
    }

    public void ResetPlayerPosition()
    {
        ActiveX = BasePositionX;
        ActiveY = BasePositionY;
    }

    private bool IsBlockCollision(List<GameObject> activeProjectGameObjects, int targetX, int targetY)
    {
        foreach (GameObject obj in activeProjectGameObjects)
        {
            if (obj != this && obj.BasePositionX == targetX && obj.BasePositionY == targetY)
            {
                return true;
            }
        }

        return false;
    }

}
