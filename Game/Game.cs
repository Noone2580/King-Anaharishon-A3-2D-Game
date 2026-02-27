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
    Color lightPink = new Color(242, 165, 172);
    Color darkPink = new Color(224, 108, 118);
    Color brown = new Color(59, 48, 45);

    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        // Set up window
        Window.SetTitle("Lady Bacon");
        Window.SetSize(400, 400);
        // Remove outlines
        Draw.LineColor = Color.Clear;
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        // Reset background
        Window.ClearBackground(Color.OffWhite);

        // 
        Draw.Capsule(140, 192, 250, 187, 75);

        // 
        Draw.FillColor = brown;
        Draw.Rectangle(122, 278, 25, 12);
        Draw.Rectangle(223, 273, 25, 11);
        Draw.Rectangle(175, 288, 30, 12);
        Draw.Rectangle(265, 278, 30, 12);

        // 
        Draw.FillColor = lightPink;
        Draw.Quad(120, 250, 150, 250, 145, 290, 125, 290);
        Draw.Quad(215, 240, 255, 240, 245, 275, 225, 275);
        Draw.Quad(170, 250, 210, 250, 203, 300, 177, 300);
        Draw.Quad(260, 240, 300, 240, 293, 290, 267, 290);

        // 
        Draw.FillColor = Color.LightGray;
        Draw.Ellipse(200, 290, 260, 40);

        // 
        Draw.FillColor = darkPink;
        Draw.Triangle(50, 120, 110, 110, 70, 155);
        Draw.Triangle(135, 100, 190, 110, 140, 150);

        // 
        Draw.FillColor = lightPink;
        Draw.Triangle(360, 130, 335, 170, 300, 150);

        // 
        Draw.FillColor = darkPink;
        Draw.Ellipse(105, 200, 50, 40);
        Draw.FillColor = brown;
        Draw.Ellipse(97, 200, 10, 20);
        Draw.Ellipse(113, 200, 10, 20);

        // 
        Draw.FillColor = lightPink;
        Draw.Rectangle(90, 210, 50, 15);
        Draw.FillColor = darkPink;
        Draw.Ellipse(115, 225, 45, 30);

        // 
        Draw.FillColor = darkPink;
        Draw.Circle(80, 170, 12);
        Draw.Circle(150, 170, 12);
    }
}