using MohawkGame2D;
using System;
using System.Numerics;

/// <summary>
///     Effect Manager script this is where all the Effects logic happens
/// </summary>

public class EffectManager
{
    Vector4[] KOs = new Vector4[50];
    int[] KOAnimIndex = new int[50];
    Texture2D KOTexture;
    Texture2D KOTextureForWord;
    Texture2D KOTextureBackWord;

    float[] Timers = new float[50];

    public void SetUp() // Script Setup
    {
        KOTextureForWord = Graphics.LoadTexture("../../../Assets/Effects/KO_Effect_Sidewas.png");
        KOTextureBackWord = Graphics.LoadTexture("../../../Assets/Effects/KO_Effect_Sidewas_Inver.png");
        KOTexture = KOTextureForWord;
    }

    public bool IsTimerDone(int TimerIndex) // For Checking if a timer is done
    {
        if (Time.SecondsElapsed >= Timers[TimerIndex])
        {
            Timers[TimerIndex] = 0;
            return true;
        }
        else
            return false;
    }

    public void SetTimer(int TimerIndex, float setTime) // sets a new timer
    {
        if (Timers[TimerIndex] <= 0)
        {
            Timers[TimerIndex] = setTime + Time.SecondsElapsed;
        }
    }

    void KOanimUpdate(int index, bool Backword) // Update the KO Animations
    {
        if (IsTimerDone(index))
        {
            SetTimer(index, 0.0909090909090909f);
            KOAnimIndex[index]++;
            if (KOAnimIndex[index] > 7)
            {
                KOs[index].W = float.Clamp(KOs[index].W - 1, 0, 7 );
            }
            else
            {
                KOs[index].W++;
            }
            if (KOAnimIndex[index] > 15)
            {
                KOs[index] = Vector4.Zero;
                KOAnimIndex[index] = 0;
            }
        }

        if (Backword)
        {
            KOTexture = KOTextureBackWord;
        }
        else KOTexture = KOTextureForWord;
    }

    public void NewKO(Vector2 Postion, Vector2 Direction, float ROW) // sets a new KO
    {
        int Index = Array.IndexOf(KOs, new Vector4(0,0,0,0));
        KOs[Index] = new Vector4(Postion, -45 + ROW, 0);
        KOAnimIndex[Index] = 0;
    }

    public void DrawEffects() // Render all effects
    {
        Graphics.Rotation = 0; // for now it just resets the image rotation
    }

    public void DrawKOs() // Render KO effect
    {
        for (int i = 0; i < KOs.Length; i++)
        {

            if (KOs[i] != new Vector4(0, 0, 0, 0))
            {
                bool Back = false;

                if (KOs[i].X > Window.Width / 2)
                {
                    Back = true;
                }

                Graphics.Scale = 2.5f;
                Graphics.Rotation = KOs[i].Z;
                Graphics.DrawSubset(KOTexture, new Vector2(KOs[i].X, KOs[i].Y), new Vector2(128 * KOs[i].W, 0), new Vector2(128));
                KOanimUpdate(i, Back);

            }
        }
        Graphics.Rotation = 0;
    }
}
