using MohawkGame2D;
using System;
using System.Drawing;
using System.Numerics;

// Made By Payton
public class SparkPlug : PlayerMaster
{
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
        base.SpecialAttack();
    }
    public override void Attack()
    {
        base.Attack();
    }
}
