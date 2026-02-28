// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    // Place your variables here:
    public int MaxLives = 3;

    public float Gravtiy = 25;

    float MaxFallSpeed = 30;

    float HitVelocity = 10;

    PlayerMaster[] Players = new PlayerMaster[2];


    public Vector2 FloorCol(Vector2 ColPos, Vector2 ColSize)
    {
        Vector2 Floor = new Vector2(0);

        if (ColPos.X + ColSize.X > 150 && ColPos.X < 650 && ColPos.Y + ColSize.Y > 400)
        {
            Floor.Y = 400;
        }


        return Floor;
    }

    public void DealDamage(float Damage, Vector2 Postion, Vector2 Size, Vector2 Direction, PlayerMaster PlayerAttacking)// Deal Damage to other players
    {
        if (PlayerAttacking != null)// Check if attacking player exists 
        {
            for (int i = 0; i < Players.Length; i++)// Go thorght all Players
            {
                if (Players[i] != null)// Check if attacked player exists 
                {
                    if (Players[i] != PlayerAttacking)// Check if attacking player is not attacked 
                    {
                        if (Players[i].Position.X + Players[i].Size.X > Postion.X 
                            && Players[i].Position.X < Postion.X + Size.X 
                            && Players[i].Position.Y + Players[i].Size.Y > Postion.Y 
                            && Players[i].Position.Y < Postion.Y + Size.Y)
                        {
                            Players[i].TakeDamage(Direction, Damage);
                            Console.WriteLine("Damage");

                        }
                        // Debug Hit Box
                        Draw.FillColor = Color.Black;
                        Draw.Rectangle(Postion, Size);

                    }
                }
            }
        }
    }


    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        Window.SetTitle("Fight!");
        Window.SetSize(800, 600);

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new PlayerMaster();// TEST FUNC REMOVE SOON

            Players[i].game = this;
            Players[i].Lives = 4;
            Players[i].PIndex = i;
            Players[i].HitVelocity = HitVelocity;
            Players[i].Position = new Vector2(Window.Width / 2, Window.Height);

        }

    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        // Reset screen
        Window.ClearBackground(Color.White);

        Draw.FillColor = Color.Black;
        Draw.Rectangle(150, 400, 500, 200);

        for (int i = 0; i < Players.Length; i++)
        {

            Players[i].DrawPlayer();
        }

        // Input Test

        if (Input.IsKeyboardKeyPressed(KeyboardInput.Space))
            Players[0].Attack();


        if (Input.IsKeyboardKeyPressed(KeyboardInput.Up))
        {
            Players[0].Jump();
        }

        if (Input.IsKeyboardKeyDown(KeyboardInput.Left))
        {
            Players[0].MoveLeft();
        }

        if (Input.IsKeyboardKeyDown(KeyboardInput.Right))
        {
            Players[0].MoveRight();
        }

        
    }
}

