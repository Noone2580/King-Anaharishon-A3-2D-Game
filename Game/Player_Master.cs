using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

public class PlayerMaster
{

    public virtual PlayerMaster NewSelf()
    {
        return new PlayerMaster();
    }

    // Game Info
    public Game game;
    public int PIndex = 0;
    public string Name = "V-BOT";
    int FC = 0;
    float[] Timers = new float[100];


    // Anim and size
    public string[] TexureLocations { get; set; } = ["../../../Assets/Characters/VBot/FN_VBOT_Idle.png",// Idle = 0
        "../../../Assets/Characters/VBot/FN_VBOT_Moving.png", // Moving = 1
        "../../../Assets/Characters/VBot/FN_VBOT_Air.png",// Air = 2
        "../../../Assets/Characters/VBot/FN_VBOT_LightHitForword.png",// Hit = 3
        "../../../Assets/Characters/VBot/FN_VBOT_LightHitUp.png",// Hit Up = 4
        "../../../Assets/Characters/VBot/FN_VBOT_LightHitDown.png"];// Hit Down = 5

    public string[] TexureLocationsBack { get; set; } = ["../../../Assets/Characters/VBot/FN_VBOT_Idle_Back.png",// Idle = 0
        "../../../Assets/Characters/VBot/FN_VBOT_Moving_Back.png", // Moving = 1
        "../../../Assets/Characters/VBot/FN_VBOT_Air_Back.png",// Air = 2
        "../../../Assets/Characters/VBot/FN_VBOT_LightHitBackword.png",// Hit = 3
        "../../../Assets/Characters/VBot/FN_VBOT_LightHitUp_Back.png",// Hit Up = 4
        "../../../Assets/Characters/VBot/FN_VBOT_LightHitDown_Back.png"];// Hit Down = 5
    public string[] PortraitTexturesLocations { get; set; } = ["../../../Assets/Characters/VBot/FN_VBot_Portrait.png", "../../../Assets/Characters/VBot/FN_VBot_Portrait_2.png"];
    public Texture2D[] PortraitTexures { get; set; } = new Texture2D[2];


    public Vector2 SpritSize { get; set; } = new Vector2(128, 128);
    Vector2 AnimOffeset;
    int AnimOffIndex = 0;
    public int MaxAnimOffIndex = 3;
    int AnimIndex = 0;
    public int AnimFrameRate { get; set; } = 6;

    public Texture2D[] PlayerTexures = new Texture2D[8];
    public Texture2D[] PlayerTexuresB = new Texture2D[8];
    public Vector2 Size = new Vector2(50, 75);

    // Movement Vars
    public float MoveSpeed { get; set; } = 100;
    public Vector2 Position = new Vector2();
    public Vector2 Velocity = new Vector2();
    public Vector2 LastDirection = new Vector2(1, 0);
    public bool LockMovement = false;

    // Jumping Vars
    public float JumpForce { get; set; } = -700;
    public int MaxJumps { get; set; } = 2;
    int NumJumps = 0;

    // Life And Damage
    public int Lives = 3;
    public float DHp = 0;
    public float BaseDamage { get; set; } = 10;
    public float HitVelocity;
    public float DamageStunTime { get; set; } = .05f;
    public float AttackRecoveryTime { get; set; } = .4f;



    bool IsAttacking;
    bool Hit;
    bool IsOnGround;

    void AnimUpdate()
    {
        if (AnimOffIndex == 0) { AnimOffeset.X = 0; }
        if (CalTiggerPerSec(AnimFrameRate, FC))
        {
            if (AnimOffIndex < MaxAnimOffIndex)
            {
                AnimOffeset.X += 128;
                AnimOffIndex++;
            }
            else
            {
                AnimOffeset.X = 0;
                AnimOffIndex = 0;
                if (IsAttacking)
                    IsAttacking = false;
            }
        }
        if (!IsAttacking)
        {
            if (IsOnGround)
            {
                if (Velocity.X < 3 && Velocity.X > -3)
                {
                    AnimIndex = 0;
                }
                else if (Velocity.X > 0 || Velocity.X < 0)
                {
                    AnimIndex = 1;
                }
            }
            else
            {
                AnimIndex = 2;
            }
        }
    }

    bool CalTiggerPerSec(int Rate, int CurrentFrame)
    {
        if (Rate <= 0)
            return false;

        int FKf = 60 / Rate - 1;

        // Goes throgh all Rate to find if FKf is even to the Current Frame.
        // If so return true.
        for (int i = 0; i < Rate; i++)
        {
            if (FKf == CurrentFrame)
            {
                return true;
            }
            else
                FKf = FKf + (60 / Rate) - 1;


        }
        return false;
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


    public void Setup()
    {
        SetCustomVars();
        for (int i = 0; i < TexureLocations.Length; i++)
        {
            PlayerTexures.SetValue(Graphics.LoadTexture(TexureLocations[i]), i);
            PlayerTexuresB.SetValue(Graphics.LoadTexture(TexureLocationsBack[i]), i);
        }

        for (int i = 0; i < PortraitTexturesLocations.Length; i++)
        {
            PortraitTexures.SetValue(Graphics.LoadTexture(PortraitTexturesLocations[i]), i);
        }
    }

    public virtual void SetCustomVars()
    {

    }

    void Col()// Floor Collsion Check
    {
        Vector4 FCol = game.FloorCol(Position, Size);

        if (FCol.X != 0)
        {
            if (FCol.Z < 0)
                Position.X = FCol.X;

            if (FCol.Z > 0)
                Position.X = FCol.X - Size.X;

            if (Hit)
            {
                Velocity.X = -Velocity.X * .7f;

            }
            else
            {
                Velocity.X = 0;
                Velocity.Y = 0;
            }

            NumJumps = 0;
            IsOnGround = true;

            return;
        }
        if (FCol.Y != 0)
        {
            Position.Y = FCol.Y - Size.Y;

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

    public virtual void TakeDamage(Vector2 Direction, float Damage)// Take Damage
    {
        Hit = true;
        DHp += Damage;
        Velocity += Direction * (HitVelocity * DHp);
    }

    public virtual void Attack() // Attack
    {
        if (IsTimerDone(0))
        {
            SetTimer(0, AttackRecoveryTime);
            Vector2 TempPos = Position;
            IsAttacking = true;
            AnimOffIndex = 0;
            FC = 0;
            Velocity += LastDirection * 200;
            LockMovement = true;

            if (LastDirection.X > 0)
            {
                AnimIndex = 3;
                TempPos.X = Position.X + Size.X / 2;
                game.DealDamage(BaseDamage, TempPos, Size, LastDirection, DamageStunTime, this);
                return;
            }
            if (LastDirection.X < 0)
            {
                AnimIndex = 3;
                TempPos.X = Position.X - Size.X / 2;
                game.DealDamage(BaseDamage, TempPos, Size, LastDirection, DamageStunTime, this);
                return;
            }
            if (LastDirection.Y > 0)
            {
                AnimIndex = 5;
                TempPos.Y = Position.Y + Size.Y / 2;
                game.DealDamage(BaseDamage, TempPos, Size, LastDirection, DamageStunTime, this);
                return;
            }
            if (LastDirection.Y < 0)
            {
                AnimIndex = 4;
                TempPos.Y = Position.Y - Size.Y / 2;
                game.DealDamage(BaseDamage, TempPos, Size, LastDirection, DamageStunTime, this);
                return;
            }
        }
    }
    public virtual void SpecialAttack()// Special Attack
    {

    }


    public virtual void Die()
    {
        Hit = false;
        NumJumps = 0;
        DHp = 0;
        Lives--;
        Velocity = new Vector2();
        Position = new Vector2(Window.Width / 2, 10);
    }

    // Movement
    public virtual void Jump()
    {
        if (!LockMovement)
        {
            if (NumJumps < MaxJumps)
            {
                Velocity.Y = JumpForce;
                NumJumps++;
            }
            LastDirection = new Vector2(0, -1);
        }
    }
    public virtual void MoveLeft()
    {
        if (!LockMovement)
        {
            Velocity.X += -MoveSpeed;
            LastDirection = new Vector2(-1, 0);
        }
    }
    public virtual void MoveRight()
    {
        if (!LockMovement)
        {
            Velocity.X += MoveSpeed;
            LastDirection = new Vector2(1, 0);
        }
    }
    public virtual void MoveDown()
    {
        if (!LockMovement)
        {
            Velocity.Y += MoveSpeed / 3;
            LastDirection = new Vector2(0, 1);
        }
    }

    public virtual void MoveUp()
    {
        if (!LockMovement)
        {
            LastDirection = new Vector2(0, -1);
        }
    }
    // Movement End


    public virtual void DrawPlayer()// Render Player
    {
        Velocity += new Vector2(0, game.Gravtiy);


        if (Hit)
        {
            LockMovement = true;
            Velocity.X = Velocity.X * .95f;

            Vector2 CalVel = Velocity;

            if (CalVel.X < 0) { CalVel.X = -CalVel.X; }
            if (CalVel.Y < 0) { CalVel.Y = -CalVel.Y; }

            if (CalVel.X < MoveSpeed && CalVel.Y < MoveSpeed)
            {
                Hit = false;
                LockMovement = false;
            }
        }
        else
        {
            Velocity.X = Velocity.X * .8f;
            if (IsTimerDone(0))
                LockMovement = false;
        }

        Position += Velocity * Time.DeltaTime;
        Col();

        AnimUpdate();
        Graphics.Scale = .85f;
        if (Velocity.X > 0)
        {
            Graphics.DrawSubset(PlayerTexures[AnimIndex], new Vector2(Position.X - Size.X * .6f, Position.Y - Size.Y * .3f), AnimOffeset, new Vector2(128, 128));
        }
        else
            Graphics.DrawSubset(PlayerTexuresB[AnimIndex], new Vector2(Position.X - Size.X * .6f, Position.Y - Size.Y * .3f), AnimOffeset, new Vector2(128, 128));

        FC++;
        if (FC >= 60)
        {
            FC = 0;
        }
    }

    public virtual void DrawPlayerNoUpdate()
    {
        Graphics.Scale = .85f;
        if (Velocity.X > 0)
        {
            Graphics.DrawSubset(PlayerTexures[AnimIndex], new Vector2(Position.X - Size.X * .6f, Position.Y - Size.Y * .3f), AnimOffeset, new Vector2(128, 128));
        }
        else
            Graphics.DrawSubset(PlayerTexuresB[AnimIndex], new Vector2(Position.X - Size.X * .6f, Position.Y - Size.Y * .3f), AnimOffeset, new Vector2(128, 128));
    }
}
