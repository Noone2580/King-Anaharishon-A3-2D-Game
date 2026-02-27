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
    Vector2[] PlayerVelocity = new Vector2[4];
    Vector2[] PlayerCords = new Vector2[4];
    float[] PlayerDHP = new float[4];
    int[] PlayerLives = new int[4];



    public void DrawPlayer()
    {
        Draw.FillColor = Color.Red;
        Draw.Circle(PlayerCords[0], 50);
    }

    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        Window.SetTitle("Fight!");
        Window.SetSize(800, 600);
        PlayerCords[0] = new Vector2(0, 0);

    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        // Reset screen
        Window.ClearBackground(Color.White);

        DrawPlayer();
    }
}

