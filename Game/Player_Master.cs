using MohawkGame2D;
using System;
using System.Numerics;
using System.Reflection;

public class PlayerMaster
{
    public Game game;

    Vector2 Size = new Vector2(25, 25);

    public Vector2 Position = new Vector2();
    public Vector2 Velocity = new Vector2();

    public float DHp = 0;

    public int PIndex = 0;

    public int Lives = 0;


    void Col()
    {
        Vector2 FCol = game.FloorCol(Position, Size);

        //if (FCol.X != 0)
        //{
        //    Position.X = game.FloorCol(Position, Size).X - Size.X;

        //    Velocity.X = 0;
        //}
        if (FCol.Y != 0)
        {
            Position.Y = game.FloorCol(Position, Size).Y - Size.Y;

            Velocity.Y = 0;
        }
    }

    public void Jump()
    {
        Velocity.Y = -200;
    }
    public void MoveLeft()
    {
        Velocity.X -= 10;
    }
    public void MoveRight()
    {
        Velocity.X += 10;
    }


    public void DrawPlayer()
    {

        Velocity += new Vector2(0, 25);

        Position += Velocity * Time.DeltaTime;

        Col();

        Draw.FillColor = Color.Red;
        Draw.Rectangle(Position, Size);
    }
}
