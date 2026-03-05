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
    public float Gravtiy = 20;
    float OutOfBoundsRange = 100;
    float HitVelocity = 5f;
    public int FC = 0;
    float[] Timers = new float[100];
    Texture2D BackGround;

    EffectManager Effects = new EffectManager();


    // DEBUG\
    Texture2D Airrow;
    //

    // Player Settings
    public PlayerMaster[] Players { get; set; } = new PlayerMaster[4];
    public int MaxLives = 3;
    float DeadZone = .5f;

    // Menu
    CharacterMenu menu = new CharacterMenu();

    public Vector4 FloorCol(Vector2 ColPos, Vector2 ColSize)
    {
        Vector4 Floor = new Vector4(0);

        if (ColPos.X < 650 && ColPos.X + 5 > 650 && ColPos.Y + ColSize.Y > 400)
        {
            Floor.X = 650;
            Floor.Z = -1;
            return Floor;
        }

        if (ColPos.X + ColSize.X > 150 && ColPos.X + ColSize.X - 5 < 150 && ColPos.Y + ColSize.Y > 400)
        {
            Floor.X = 150;
            Floor.Z = 1;
            return Floor;
        }

        if (ColPos.X + ColSize.X > 150 && ColPos.X < 650 && ColPos.Y + ColSize.Y > 400)
        {
            Floor.Y = 400;
            Floor.W = 1;
            return Floor;
        }

        return Floor;
    }

    public void DealDamage(float Damage, Vector2 Postion, Vector2 Size, Vector2 Direction, float StunTime, PlayerMaster PlayerAttacking)// Deal Damage to other players
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
                            SetTimer(i, StunTime);
                        }

                    }
                }
            }
        }
    }


    public void Setup()
    {
        Window.SetTitle("Fight Night All Stars!");
        Window.SetSize(800, 600);
        BackGround = Graphics.LoadTexture("../../../Assets/BackGrounds/MainStage.png");

        Effects.SetUp();

        Airrow = Graphics.LoadTexture("../../../Assets/Airrow.png");

        menu.Setup(this);
        //GameStart();
    }

    public void GameStart()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].Setup();
            Players[i].game = this;
            Players[i].Lives = MaxLives;
            Players[i].PIndex = i;
            Players[i].HitVelocity = HitVelocity;
            Players[i].Position = new Vector2(Window.Width / 2, Window.Height);

        }

        GameState = 1;
    }


    public bool IsTimerDone(int TimerIndex)
    {
        if (Time.SecondsElapsed >= Timers[TimerIndex])
        {
            Timers[TimerIndex] = 0;
            return true;
        }
        else
            return false;
    }

    public void SetTimer(int TimerIndex, float setTime)
    {
        if (Timers[TimerIndex] <= 0)
        {
            Timers[TimerIndex] = setTime + Time.SecondsElapsed;
        }
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
            Graphics.Rotation = 0;
            Graphics.Scale = 1;
            Graphics.Draw(BackGround, Vector2.Zero);

            //Draw.FillColor = Color.Black;
            //Draw.Rectangle(150, 400, 500, 200);

            for (int i = 0; i < Players.Length; i++)
            {
                int NumAlive = 0;

                if (Players[i] != null)
                {
                    if (Players[i].Lives >= 0)
                    {
                        NumAlive++;

                        Vector2 Pos = new Vector2((200 * i) + 150 + 10 * i, 550);

                        Text.Color = Color.DarkGray;
                        Draw.FillColor = Color.White;

                        Text.Size = 70;
                        Graphics.Scale = .8f;

                        Draw.Circle(Pos, 50);
                        Graphics.Draw(Players[i].PortraitTexures[0], Pos - new Vector2(50));
                        Text.Draw($"{Players[i].DHp}%", Pos.X - 50, Pos.Y - 25);

                        Text.Color = Color.Green;
                        Text.Size = 60;
                        Text.Draw($"{Players[i].Lives}", Pos.X + 50, Pos.Y - 25);

                        if (Players[i].Position.Y > Window.Height + OutOfBoundsRange || Players[i].Position.Y + Players[i].Size.Y < 0 - OutOfBoundsRange || Players[i].Position.X + Players[i].Size.X < 0 - OutOfBoundsRange || Players[i].Position.X > Window.Width + OutOfBoundsRange) // Kill player if off screen
                        {
                            Effects.NewKO(Players[i].Position, Vector2.Normalize(Players[i].Velocity));

                            Players[i].Die();
                        }




                        // DEBUG
                        Vector2 Direction = Vector2.Normalize(Players[i].Velocity);

                        float DEEEE = float.RadiansToDegrees(MathF.Sin(Direction.Y / Direction.X) * -1);


                        Draw.LineColor = Color.White;
                        Draw.Line(Players[i].Position.X, Players[i].Position.Y, Players[i].Position.X + (Direction.X * 50), Players[i].Position.Y + (Direction.Y * 50));

                        Graphics.Rotation = -DEEEE;
                        Graphics.Scale = 5;
                        Graphics.Draw(Airrow, Players[i].Position);

                        Graphics.Scale = 1;
                        Graphics.Rotation = 0;
                        // DEBUG






                        if (IsTimerDone(i))// If the player is not stuned
                        {
                            Players[i].DrawPlayer();

                            // Then Move
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
                        else Players[i].DrawPlayerNoUpdate();
                    }
                }

                if (NumAlive <= 0) { GameState = 0; }
            }


            Effects.DrawEffects();
            FC++;
            if (FC >= 60)
            {
                FC = 0;
            }
            return;
        }
    }
}

