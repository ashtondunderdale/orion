using engine_new;
using System.Drawing;
using System.Xml.Linq;

internal class Chaser : GameObject
{
    private readonly Timer chaseTimer;
    private readonly SemaphoreSlim moveSemaphore = new SemaphoreSlim(1, 1);

    public int ActiveX;
    public int ActiveY;

    public Chaser(int x, int y, string name)
    {
        BasePositionX = x;
        BasePositionY = y;
        ActiveX = BasePositionX;
        ActiveY = BasePositionY;

        Colour = ConsoleColor.Red;
        Symbol = "0";
        Name = name;

        chaseTimer = new Timer(ChasePlayerCallback, null, 0, 1000); // fix this to move every 1 second max
    }

    private void ChasePlayerCallback(object state)
    {
        if (moveSemaphore.Wait(0))
        {
            try
            {
                ChasePlayer((Player)state);
            }
            finally
            {
                moveSemaphore.Release();
            }
        }
    }

    public void ChasePlayer(Player player)
    {
        if (player is not null)
        {
            int directionX = Math.Sign(player.ActiveX - ActiveX);
            int directionY = Math.Sign(player.ActiveY - ActiveY);

            MoveChaser(directionX, directionY);
        }
    }

    private void MoveChaser(int directionX, int directionY)
    {
        Console.SetCursorPosition(ActiveX, ActiveY);
        Console.Write(' ');

        ActiveX += directionX;
        ActiveY += directionY;

        Console.SetCursorPosition(ActiveX, ActiveY);
        Console.ForegroundColor = Colour;
        Console.Write(Symbol);
        Console.ResetColor();
    }
}
