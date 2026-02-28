using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

public class PlayerMaster
{
    public Game game;

    public int PIndex = 0;

    public Vector2 Size = new Vector2(25, 35);

    // Movement Vars
    public float MoveSpeed = 150;
    public Vector2 Position = new Vector2();
    public Vector2 Velocity = new Vector2();
    Vector2 LastDirection = new Vector2(1,0);


    // Jumping Vars
    public float JumpForce = -700;
    public int MaxJumps = 2;
    int NumJumps = 0;

    // Life And Damage
    public int Lives = 3;
    public float DHp = 0;
    public float BaseDamage = 10;
    public float HitVelocity;

    bool Hit;
    bool IsOnGround;

    void Col()// Floor Collsion Check
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

            if (Hit)
            {
                Velocity.Y = -Velocity.Y * .7f;

            }
            else
                Velocity.Y = 0;

            NumJumps = 0;
            IsOnGround = true;
        }
        else { IsOnGround = false; }
    }

    public void TakeDamage(Vector2 Direction, float Damage)// Take Damage
    {
        Hit = true;
        DHp += Damage;
        Velocity += Direction * (HitVelocity * DHp);
        Console.WriteLine(Direction);
    }

    public void Attack() // Attack
    {
        Vector2 TempPos = Position;

        if (LastDirection.X > 0)
        {
            TempPos.X = Position.X + Size.X / 2;
            game.DealDamage(BaseDamage, TempPos, Size, LastDirection, this);
            return;
        }
        if (LastDirection.X < 0)
        {
            TempPos.X = Position.X - Size.X / 2;
            game.DealDamage(BaseDamage, TempPos, Size, LastDirection, this);
            return;
        }
        if (LastDirection.Y > 0)
        {
            TempPos.Y = Position.Y + Size.Y / 2;
            game.DealDamage(BaseDamage, TempPos, Size, LastDirection, this);
            return;
        }
        if (LastDirection.Y < 0)
        {
            TempPos.Y = Position.Y - Size.Y / 2;
            game.DealDamage(BaseDamage, TempPos, Size, LastDirection, this);
            return;
        }
    }

    public void Die() 
    {
        Hit = false;
        NumJumps = 0;
        DHp = 0;
        Lives--;
        Velocity = new Vector2();
        Position = new Vector2(Window.Width/2, 10);
    }

    // Movement
    public void Jump()
    {
        if (NumJumps < MaxJumps)
        {
            Velocity.Y = JumpForce;
            NumJumps++;
        }
        LastDirection = new Vector2(0,-1);
    }
    public void MoveLeft()
    {
        Velocity.X += -MoveSpeed;
        LastDirection = new Vector2(-1,0);

    }
    public void MoveRight()
    {
        Velocity.X += MoveSpeed;
        LastDirection = new Vector2(1,0);

    }
    public void MoveDown() 
    {
        Velocity.Y += MoveSpeed/2;
        LastDirection = new Vector2(0,1);
    }
    // Movement End


    public void DrawPlayer()// Render Player
    {
        Velocity += new Vector2(0, game.Gravtiy);


        if (Hit)
        {
            Velocity.X = Velocity.X * .9f;

            Vector2 CalVel = Velocity;

            if (CalVel.X < 0) { CalVel.X = -CalVel.X; }
            if (CalVel.Y < 0) { CalVel.Y = -CalVel.Y; }

            if (CalVel.X < MoveSpeed && CalVel.Y < MoveSpeed)
            {
                Hit = false;
            }
        }
        else
        {
            Velocity.X = Velocity.X * .8f;

        }

        Position += Velocity * Time.DeltaTime;

        Col();

        Draw.FillColor = Color.Red;
        Draw.Rectangle(Position, Size);
    }
}
