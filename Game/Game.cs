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

    PlayerMaster[] Players = new PlayerMaster[1];


    public Vector2 FloorCol(Vector2 ColPos, Vector2 ColSize)
    {
        Vector2 Floor = new Vector2();

        if (ColPos.X + ColSize.X > 150 && ColPos.X - ColSize.X < 650 && ColPos.Y + ColSize.Y > 400)
            return Floor;
        else
            return Floor;
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

            Players[i].Lives = 4;
            Players[i].PIndex = i;
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

        Draw.FillColor= Color.Black;
        Draw.Rectangle(150 , 400, 500 , 200);

        // Input Test
        
        if (Input.IsKeyboardKeyPressed(KeyboardInput.Up)) 
        {
            Players[0].Velocity.Y = -500;
        }

        for (int i = 0; i < Players.Length; i++)
        {

            Players[i].DrawPlayer();
        }
    }
}

