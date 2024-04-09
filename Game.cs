﻿namespace engine;

internal class Game : Engine
{
    public static Player Player;
    public static bool Play = false;

    public override void OnLoad()
    {
        Player = new(new Vector2(400, 400), new Vector2(10, 10), "player");
    }

    public override void OnDraw()
    {

    }

    public override void OnUpdate()
    {
        Vector2 newPosition = new(
            Player.PlayPosition.X + (Player.Left ? -Player.Velocity : Player.Right ? Player.Velocity : 0),
            Player.PlayPosition.Y + (Player.Up ? -Player.Velocity : Player.Down ? Player.Velocity : 0)
        );

        if (!Player.IsColliding(newPosition))
            Player.PlayPosition = newPosition;
    }

    public override void GetKeyDown(KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.Z)
        {
            if (Play) return;

            if (Shapes.Count > 0)
                UnregisterShape(Shapes[Shapes.Count - 1]);
        }

        if (!Play) return;

        if (e.KeyCode == Keys.W)
            Player.Up = true;

        else if (e.KeyCode == Keys.S)
            Player.Down = true;

        else if (e.KeyCode == Keys.A)
            Player.Left = true;

        else if (e.KeyCode == Keys.D)
            Player.Right = true;
    }

    public override void GetKeyUp(KeyEventArgs e)
    {
        if (!Play) return;

        if (e.KeyCode == Keys.W)
            Player.Up = false;

        else if (e.KeyCode == Keys.S)
            Player.Down = false;

        else if (e.KeyCode == Keys.A)
            Player.Left = false;

        else if (e.KeyCode == Keys.D)
            Player.Right = false;
    }
}
