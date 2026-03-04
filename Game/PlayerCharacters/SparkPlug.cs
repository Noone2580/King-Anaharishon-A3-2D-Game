using MohawkGame2D;
using System;
using System.Drawing;
using System.Numerics;

// Made By Payton
public class SparkPlug : PlayerMaster
{
    bool Targeting = false;

    Vector2 TargetPostion = Vector2.Zero;
    float TargetingMoveSpeed = 250;
    Vector2 TargetSize = new Vector2(50);

    int TargetingStage = 0;
    float TargetingDamage = 100;
    float TargetCharageSpeed = 10;

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
        AttackRecoveryTime = .2f;

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
    }

    public override void SpecialAttack()
    {
        if (Targeting)
        {
            game.DealDamage(TargetingDamage, TargetPostion, TargetSize, new Vector2(1, 1), .3f, this);
            LockMovement = false;
            Targeting = false;
        }

        if (LastDirection.Y > 0)
        {
            if (IsTimerDone(5))
            {
                LockMovement = true;
                Targeting = true;
                TargetPostion = Position;
                SetTimer(0, 3);
                SetTimer(5, 6);
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
            }
        }
        else
        {
            base.DrawPlayer();

        }
    }
}
