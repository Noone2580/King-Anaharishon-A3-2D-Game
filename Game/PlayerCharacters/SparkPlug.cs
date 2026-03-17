using MohawkGame2D;
using System;
using System.Drawing;
using System.Numerics;

// Made By Payton

/// <summary>
///     Player Character Sprk Plug
/// </summary>
public class SparkPlug : PlayerMaster
{
    bool Targeting = false;

    Vector2 TargetPostion = Vector2.Zero;
    float TargetingMoveSpeed = 800;
    Vector2 TargetSize = new Vector2(50);

    int TargetingStage = 0;
    float TargetingDamage = 150;
    float TargetCharageSpeed = 10;

    string TargetTextureLocation = "../../../Assets/Characters/SparkPlug/SparkTarget.png";
    Texture2D TargetTexture;

    public override PlayerMaster NewSelf()
    {
        return new SparkPlug();
    }

    // When Modafing Vars set them here in the SetCustomVars Function
    public override void SetCustomVars() // When Modafing Functions if possable use OVERRIDE
    {
        MoveSpeed = 50;
        JumpForce = -800;
        MaxJumps = 2;

        MaxAnimOffIndex = 2;
        AnimFrameRate = 5;

        BaseDamage = 2;
        DamageStunTime = .3f;
        AttackRecoveryTime = .4f;

        Name = "Spark Plug";
        PortraitTexturesLocations = ["../../../Assets/Characters/SparkPlug/Sparkplug_Portarit_2.png", "../../../Assets/Characters/SparkPlug/Sparkplug_Portarit.png"];


        TexureLocations = ["../../../Assets/Characters/SparkPlug/Sparkplug_Idle.png",// Idle = 0
        "../../../Assets/Characters/SparkPlug/Sparkplug_MovingRight.png", // Moving = 1
        "../../../Assets/Characters/SparkPlug/Sparkplug_Idle.png",// Air = 2
        "../../../Assets/Characters/SparkPlug/SparkWhip.png",// Hit = 3
        "../../../Assets/Characters/SparkPlug/SparkWhip_Up.png",// Hit Up = 4
        "../../../Assets/Characters/SparkPlug/SparkWhip_Down.png"];// Hit Down = 5

        TexureLocationsBack = ["../../../Assets/Characters/SparkPlug/Sparkplug_Idle.png",// Idle = 0
        "../../../Assets/Characters/SparkPlug/Sparkplug_MovingLeft.png", // Moving = 1
        "../../../Assets/Characters/SparkPlug/Sparkplug_Idle.png",// Air = 2
        "../../../Assets/Characters/SparkPlug/SparkWhip_Back.png",// Hit = 3
        "../../../Assets/Characters/SparkPlug/SparkWhip_Up.png",// Hit Up = 4
        "../../../Assets/Characters/SparkPlug/SparkWhip_Down.png"];// Hit Down = 5

        TargetTexture = Graphics.LoadTexture(TargetTextureLocation);
    }

    public override void SpecialAttack()
    {
        if (Targeting)
        {
            game.DealDamage(TargetingDamage, TargetPostion, TargetSize, new Vector2(1, 1), .3f, this);
            LockMovement = false;
            Targeting = false;
            SetTimer(0, 1f);
            return;
        }

        if (LastDirection.Y > 0)
        {
            if (IsTimerDone(5))
            {
                LockMovement = true;
                Targeting = true;
                TargetPostion = Position;
                SetTimer(0, 1);
                SetTimer(5, 4);
                return;
            }
        }

        if (LastDirection.X > 0 || LastDirection.X < 0)
        {

            Vector2 TempPos = Position;

            if (IsTimerDone(4))
            {
                if (LastDirection.X > 0)
                    TempPos.X = Position.X + Size.X;
                if (LastDirection.X < 0)
                    TempPos.X = Position.X - Size.X;

                LockMovement = true;
                game.DealDamage(10, TempPos, Size, LastDirection, 2, this);

                SetTimer(0, 1);
                SetTimer(4,4);
            }
        }
    }
    public override void Attack()
    {
        if (!Targeting)
        {
            base.Attack();
        }
    }

    public override void TakeDamage(Vector2 Direction, float Damage)
    {
        Targeting = false;

        base.TakeDamage(Direction, Damage);
    }

    public override void MoveUp()
    {
        base.MoveUp();
        if (Targeting)
        {
            TargetPostion.Y -= TargetingMoveSpeed * Time.DeltaTime;

        }
    }

    public override void MoveLeft()
    {
        base.MoveLeft();
        if (Targeting)
        {
            TargetPostion.X -= TargetingMoveSpeed * Time.DeltaTime;

        }
    }

    public override void MoveRight()
    {
        base.MoveRight();
        if (Targeting)
        {
            TargetPostion.X += TargetingMoveSpeed * Time.DeltaTime;

        }
    }
    public override void MoveDown()
    {
        base.MoveDown();
        if (Targeting)
        {
            TargetPostion.Y += TargetingMoveSpeed * Time.DeltaTime;
        }
    }

    public override void DrawPlayer()
    {
        if (Targeting)
        {
            if (IsTimerDone(0))
            {
                Targeting = false;
            }
            else
            {
                DrawPlayerNoUpdate();

                Draw.Rectangle(TargetPostion, TargetSize);
                Graphics.Scale = .4f;
                Graphics.DrawSubset(TargetTexture, TargetPostion, new Vector2(0, 0), new Vector2(128, 128));

            }
        }
        else
        {
            base.DrawPlayer();

        }
    }
}
