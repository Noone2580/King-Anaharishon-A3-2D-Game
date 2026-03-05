using MohawkGame2D;
using System;
using System.Drawing;
using System.Numerics;

// Made By Anaharishon
public class VBot : PlayerMaster
{
    public override PlayerMaster NewSelf()
    {
        return new VBot();
    }

    // When Modafing Vars set them here in the SetCustomVars Function
    public override void SetCustomVars() // When Modafing Functions if possable use OVERRIDE
    {
        MoveSpeed = 90;
        JumpForce = -560;
        MaxJumps = 3;
        BaseDamage = 5;
    }
}
