using MohawkGame2D;
using System;
using System.Numerics;
using System.Reflection;

public class PlayerMaster
{

    Vector2 Size = new Vector2(25, 25);

    public Vector2 Position = new Vector2();
    public Vector2 Velocity = new Vector2();

    public float DHp = 0;

    public int PIndex = 0;

    public int Lives = 0;

    public void SetupPlayer(int PlayerIndex)
    {
        PIndex = PlayerIndex;
    }

    public void DrawPlayer( )
    {

        Velocity += new Vector2(0, 25);

        Position += Velocity * Time.DeltaTime;

        //if (Game.IsFloorCol(Position, Size))
        //{
        //    Position.Y = Game.FloorCol(Position, Size).Y - Size.Y;

        //    Velocity.Y = 0;

        //}

        Draw.FillColor = Color.Red;
        Draw.Rectangle(Position, Size);
    }
}
