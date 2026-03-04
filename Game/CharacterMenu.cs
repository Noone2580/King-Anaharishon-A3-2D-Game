using MohawkGame2D;
using System;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;


public class CharacterMenu
{
    Game game;

    // Player Vars
    Vector2[] PlayerPos = new Vector2[9];
    float PlayerPawnSize = 20;
    bool[] PlayerHasCharacter = new bool[4];
    int[] PCIndex = new int[4];


    // Character Vars
    PlayerMaster[] AllCharacters = [new VBot(), new SparkPlug()];
    Vector2[] PortraitPos = new Vector2[2];
    Vector2 PortraitSizes = new Vector2(128, 60);
    Vector2[] ChararterPos = new Vector2[4];


    float PawnMoveSpeed = 400f;

    public void Setup(Game G)
    {
        game = G;
        PortraitPos = new Vector2[AllCharacters.Length];

        for (int i = 0; i < AllCharacters.Length; i++)
        {
            AllCharacters[i].SetCustomVars();

            for (int c = 0; c < AllCharacters[i].PortraitTexturesLocations.Length; c++)
            {
                AllCharacters[i].PortraitTexures[c] = Graphics.LoadTexture(AllCharacters[i].PortraitTexturesLocations[c]);
            }
            PortraitPos[i] = new Vector2((PortraitSizes.X * i) + 10 + 10 * i, 10);

        }
    }

    public void DrawMenu()
    {
        // Reset screen
        Window.ClearBackground(new Color(150, 0, 0));
        Graphics.Scale = 1;
        Text.Color = Color.Black;

        // Draw Portaits
        for (int i = 0; i < PortraitPos.Length; i++)
        {
            Draw.FillColor = Color.OffWhite;
            Draw.Rectangle(PortraitPos[i], PortraitSizes);
            Graphics.DrawSubset(AllCharacters[i].PortraitTexures[1], PortraitPos[i], new Vector2(), PortraitSizes);

        }

        // If Number of players is not equle to the number of controllers 
        if (PlayerPos.Length != Input.GetConnectedControllerCount())
        {
            // Then Resize player array
            PlayerPos = new Vector2[Input.GetConnectedControllerCount()];
            PlayerHasCharacter = new bool[Input.GetConnectedControllerCount()];

            ChararterPos = new Vector2[Input.GetConnectedControllerCount()];
            game.Players = new PlayerMaster[Input.GetConnectedControllerCount()];

            PCIndex = new int[Input.GetConnectedControllerCount()];

            for (int i = 0; i < ChararterPos.Length; i++)
            {
                ChararterPos[i] = new Vector2(200 * i, 350);
            }
        }



        for (int i = 0; i < PlayerPos.Length; i++)
        {

            if (Input.IsControllerButtonPressed(i, ControllerButton.RightFaceRight))
            {
                PlayerHasCharacter[i] = false;

                game.Players[i] = null;

            }

            if (Input.IsControllerButtonPressed(i, ControllerButton.RightFaceUp))
            {
                game.GameStart();

            }

            PlayerPos[i].X += Input.GetControllerAxis(i, ControllerAxis.LeftX) * PawnMoveSpeed * Time.DeltaTime;
            PlayerPos[i].Y += Input.GetControllerAxis(i, ControllerAxis.LeftY) * PawnMoveSpeed * Time.DeltaTime;

            // Portrait Background
            Draw.FillColor = Color.OffWhite;
            Draw.Rectangle(ChararterPos[i], new Vector2(200, 250));

            Draw.FillColor = Color.LightGray;
            Draw.Rectangle(new Vector2(ChararterPos[i].X, 550), new Vector2(200, 250));



            if (game.Players[i] == null)
            {
                for (int c = 0; c < PortraitPos.Length; c++)
                {
                    if (PlayerPos[i].X + PlayerPawnSize > PortraitPos[c].X && PlayerPos[i].X - PlayerPawnSize < PortraitPos[c].X + PortraitSizes.X && PlayerPos[i].Y + PlayerPawnSize > PortraitPos[c].Y && PlayerPos[i].Y - PlayerPawnSize < PortraitPos[c].Y + PortraitSizes.Y) // Hovring over Character
                    {
                        if (Input.IsControllerButtonPressed(i, ControllerButton.RightFaceDown))
                        {
                            PlayerHasCharacter[i] = true;
                            game.Players[i] = AllCharacters[c].NewSelf();
                            PCIndex[i] = c;
                        }

                        Graphics.Scale = 1.56f;
                        Graphics.DrawSubset(AllCharacters[c].PortraitTexures[1], ChararterPos[i], new Vector2(), new Vector2(128));
                        Text.Draw(AllCharacters[c].Name, ChararterPos[i].X + 30, 560);

                    }
                }
            }
            else
            {
                Graphics.Scale = 1.56f;

                Graphics.DrawSubset(AllCharacters[PCIndex[i]].PortraitTexures[0], ChararterPos[i], new Vector2(), new Vector2(128));
                Text.Draw(AllCharacters[PCIndex[i]].Name, ChararterPos[i].X + 30, 560);

            }

        }

        // Renders All Pawns In front
        for (int i = 0; i < PlayerPos.Length; i++)
        {
            Draw.FillColor = Color.Yellow;
            Draw.Circle(PlayerPos[i], PlayerPawnSize);
        }
    }
}
