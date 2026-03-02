using MohawkGame2D;
using System;
using System.Drawing;
using System.Numerics;
public class VBot : PlayerMaster
{

    // When Modafing Vars set them here in the SetCustomVars Function
    public override void SetCustomVars() // When Modafing Functions if possable use OVERRIDE
    {
        MoveSpeed = 90;
        JumpForce = -560;
        MaxJumps = 4;


        for (int i = 0; i < TexureLocations.Length; i++)
        {
            PlayerTexures.SetValue(Graphics.LoadTexture(TexureLocations[i]), i);
            PlayerTexuresB.SetValue(Graphics.LoadTexture(TexureLocationsBack[i]), i);
        }
    }
}
