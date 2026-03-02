using MohawkGame2D;
using System;
using System.Numerics;


public class CharacterMenu
{
    Vector2[] PlayerPos = new Vector2[4];
    float PlayerPawnSize = 20;
    bool[] PlayerHasCharacter = new bool[4];

    PlayerMaster[] AllCharacters = [new VBot()];
    Vector2[] PortraitPos = new Vector2[2];
    Vector2 PortraitSizes = new Vector2(128, 60);

    float PawnMoveSpeed = 10f;

    public void Setup()
    {
        PortraitPos = new Vector2[AllCharacters.Length];

        for (int i = 0; i < AllCharacters.Length; i++)
        {
            for (int c = 0; c < AllCharacters[i].PortraitTexturesLocations.Length; c++)
            {
                AllCharacters[i].PortraitTexures[i] = Graphics.LoadTexture(AllCharacters[i].PortraitTexturesLocations[c]);
            }
            PortraitPos[i] = new Vector2(Window.Width / 2, Window.Height / 2);
        }
    }

    public void DrawMenu()
    {
        // Reset screen
        Window.ClearBackground(new Color(150, 0, 0));


        // Draw Portaits
        for (int i = 0; i < PortraitPos.Length; i++)
        {
            Draw.FillColor = Color.OffWhite;
            Draw.Rectangle(PortraitPos[i], PortraitSizes);
            Graphics.DrawSubset(AllCharacters[i].PortraitTexures[0], PortraitPos[i], new Vector2(), PortraitSizes);

        }

        // If Number of players is not equle to the number of controllers 
        if (PlayerPos.Length != Input.GetConnectedControllerCount())
        {
            // Then Resize player array
            PlayerPos = new Vector2[Input.GetConnectedControllerCount()];
            PlayerHasCharacter = new bool[Input.GetConnectedControllerCount()];
        }



        for (int i = 0; i < PlayerPos.Length; i++)
        {

            if (Input.IsControllerButtonPressed(i, ControllerButton.RightFaceRight))
            {
                PlayerHasCharacter[i] = false;
            }

            PlayerPos[i].X += Input.GetControllerAxis(i, ControllerAxis.LeftX) * PawnMoveSpeed;
            PlayerPos[i].Y += Input.GetControllerAxis(i, ControllerAxis.LeftY) * PawnMoveSpeed;

            if (!PlayerHasCharacter[i])
            {
                for (int c = 0; c < PortraitPos.Length; c++)
                {
                    if (PlayerPos[i].X + PlayerPawnSize > PortraitPos[c].X && PlayerPos[i].X - PlayerPawnSize < PortraitPos[c].X + PortraitSizes.X && PlayerPos[i].Y + PlayerPawnSize > PortraitPos[c].Y && PlayerPos[i].Y - PlayerPawnSize < PortraitPos[c].Y + PortraitSizes.Y) // Hovring over Character
                    {
                        if (Input.IsControllerButtonPressed(i, ControllerButton.RightFaceDown))
                        {
                            PlayerHasCharacter[i] = true;
                        }
                        Console.WriteLine("Hello" + Time.DeltaTime);
                    }
                }
            }

            Draw.FillColor = Color.Yellow;
            Draw.Circle(PlayerPos[i], PlayerPawnSize);
        }




    }
}
