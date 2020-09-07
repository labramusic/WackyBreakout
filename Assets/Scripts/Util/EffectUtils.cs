using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wraps the SpeedupEffectMonitor script
public static class EffectUtils
{
    public static SpeedupEffectMonitor SpeedupEffectMonitor;

    public static bool SpeedupActive
    {
        get { return SpeedupEffectMonitor.SpeedupActive; }
        set { SpeedupEffectMonitor.SpeedupActive = value; }
    }

    public static float SpeedupFactor
    {
        get { return SpeedupEffectMonitor.SpeedupFactor; }
        set { SpeedupEffectMonitor.SpeedupFactor = value; }
    }

    public static float RemainingEffectTime
    {
        get { return SpeedupEffectMonitor.RemainingEffectTime; }
    }

    public static void Initialize()
    {
        SpeedupEffectMonitor = Camera.main.GetComponent<SpeedupEffectMonitor>();
    }
}
