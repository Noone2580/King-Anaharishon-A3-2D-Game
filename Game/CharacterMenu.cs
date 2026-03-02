using MohawkGame2D;
using System;
using System.Numerics;


public class CharacterMenu
{
    Vector2[] PlayerPos = new Vector2[4];
    PlayerMaster[] AllCharacters = [new VBot()];
    Vector2[] PortraitPos;
    Vector2 PortraitSizes = new Vector2(128, 60);

    public void Setup() 
    {
        PortraitPos = new Vector2[AllCharacters.Length];

        for (int i = 0; i < AllCharacters.Length; i++) 
        {
            for (int c = 0; c < AllCharacters[i].PortraitTexturesLocations.Length; c++) 
            {
                AllCharacters[i].PortraitTexures[i] = Graphics.LoadTexture(AllCharacters[i].PortraitTexturesLocations[c]);
            }
            PortraitPos[i] = new Vector2 (Window.Width/2, Window.Height/2);
        }
    }

    public void DrawMenu()
    {
        // Reset screen
        Window.ClearBackground(new Color(150,0,0));

        if (PlayerPos.Length != Input.GetConnectedControllerCount())
        {
            PlayerPos = new Vector2[Input.GetConnectedControllerCount()];
        }

        for (int i = 0; i < PortraitPos.Length; i++) 
        {
            Draw.FillColor = Color.OffWhite;
            Draw.Rectangle(PortraitPos[i],PortraitSizes);
            Graphics.DrawSubset(AllCharacters[i].PortraitTexures[0], PortraitPos[i], new Vector2(), PortraitSizes);

        }

    }
}
