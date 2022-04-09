using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Preferences
{
    static float doublePressThreshold_Keys = 0.66f;

    public static void SetDoublePressThreshold(float amount)
    {
        doublePressThreshold_Keys = amount;
    }

    public static float GetDoublePressThreshold()
    {
        return doublePressThreshold_Keys;
    }
}
