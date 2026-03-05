using MohawkGame2D;
using System;
using System.Numerics;

public class EffectManager
{
    Vector3[] KOs = new Vector3[50];
    Texture2D KOTexture;
    public void SetUp()
    {
        KOTexture = Graphics.LoadTexture("../../../Assets/Effects/KO_Effect.png");
    }

    public void NewKO(Vector2 Postion, Vector2 Direction)
    {
        Direction = Direction * - 1;
        KOs[0] = new Vector3(Window.Width/2, Window.Height/2, float.RadiansToDegrees( MathF.Tan(Direction.Y / Direction.X) * -1));

        Console.WriteLine(Direction);

        Console.WriteLine(float.RadiansToDegrees(MathF.Tan(Direction.Y / Direction.X) * -1) );
    }
    public void DrawEffects()
    {
        for (int i = 0; i < KOs.Length; i++)
        {
            if (KOs[i] != Vector3.Zero)
            {
                Graphics.Rotation = KOs[i].Z;
                Graphics.DrawSubset(KOTexture, new Vector2(KOs[i].X, KOs[i].Y), new Vector2(128 * 7, 0), new Vector2(128));
            }
        }
    }
}
