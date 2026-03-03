// Include the namespaces (code libraries) you need below.
using System;
using System.Drawing;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    // Simultions Settings
    int GameState = 0;
    public float Gravtiy = 25;
    float OutOfBoundsRange = 100;
    float HitVelocity = 2f;
    public int FC = 0;


    // Player Settings
    public PlayerMaster[] Players { get; set; } = new PlayerMaster[4];
    public int MaxLives = 3;
    float DeadZone = .5f;

    // Menu
    CharacterMenu menu = new CharacterMenu();

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

                        }

                    }
                }
            }
        }
    }


    public void Setup()
    {
        Window.SetTitle("Fight Night!");
        Window.SetSize(800, 600);

        menu.Setup(this);
        //GameStart();
    }

    public void GameStart()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].Setup();
            Players[i].game = this;
            Players[i].Lives = 4;
            Players[i].PIndex = i;
            Players[i].HitVelocity = HitVelocity;
            Players[i].Position = new Vector2(Window.Width / 2, Window.Height);

        }

        GameState = 1;
    }


    public void Update()
    {
        if (GameState <= 0)
        {
            menu.DrawMenu();
        }
        if (GameState == 1)// Game In Play
        {
            // Reset screen
            Window.ClearBackground(Color.White);

            Draw.FillColor = Color.Black;
            Draw.Rectangle(150, 400, 500, 200);

            for (int i = 0; i < Players.Length; i++)
            {
                Players[i].DrawPlayer();

                if (Players[i].Position.Y > Window.Height + OutOfBoundsRange || Players[i].Position.Y + Players[i].Size.Y < 0 - OutOfBoundsRange || Players[i].Position.X + Players[i].Size.X < 0 - OutOfBoundsRange || Players[i].Position.X > Window.Width + OutOfBoundsRange) // Kill player if off screen
                {
                    Players[i].Die();
                }

                if (Input.IsControllerButtonPressed(i, ControllerButton.RightFaceLeft))
                {
                    Players[i].Attack();
                }

                if (Input.IsControllerButtonPressed(i, ControllerButton.RightFaceDown))
                {
                    Players[i].SpecialAttack();
                }

                if (Input.IsControllerButtonPressed(i, ControllerButton.RightFaceUp))
                {
                    Players[i].Jump();
                }

                if (Input.GetControllerAxis(i, ControllerAxis.LeftX) < -DeadZone)
                {
                    Players[i].MoveLeft();
                }
                if (Input.GetControllerAxis(i, ControllerAxis.LeftX) > DeadZone)
                {
                    Players[i].MoveRight();
                }

                if (Input.GetControllerAxis(i, ControllerAxis.LeftY) <= -DeadZone)
                {
                    Players[i].MoveUp();
                }
                if (Input.GetControllerAxis(i, ControllerAxis.LeftY) > DeadZone)
                {
                    Players[i].MoveDown();
                }
            }

            FC++;
            if (FC >= 60)
            {
                FC = 0;
            }
            return;
        }
    }
}

