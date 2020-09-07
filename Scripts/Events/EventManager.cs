using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    private static List<PickupBlock> freezerEffectInvokers = new List<PickupBlock>();
    private static List<UnityAction<float>> freezerEffectListeners = new List<UnityAction<float>>();

    private static List<PickupBlock> speedupEffectInvokers = new List<PickupBlock>();
    private static List<UnityAction<float, float>> speedupEffectListeners = new List<UnityAction<float, float>>();

    private static List<Block> pointsAddedInvokers = new List<Block>();
    private static List<UnityAction<int>> pointsAddedListeners = new List<UnityAction<int>>();

    private static List<Ball> ballsReducedInvokers = new List<Ball>();
    private static List<UnityAction> ballsReducedListeners = new List<UnityAction>();

    private static List<Ball> ballDestroyedInvokers = new List<Ball>();
    private static List<UnityAction> ballDestroyedListeners = new List<UnityAction>();

    public static void AddFreezerEffectInvoker(PickupBlock invoker)
    {
        freezerEffectInvokers.Add(invoker);
        foreach (var listener in freezerEffectListeners)
        {
            invoker.AddFreezerEffectListener(listener);
        }
    }

    public static void AddFreezerEffectListener(UnityAction<float> listener)
    {
        freezerEffectListeners.Add(listener);
        foreach (var invoker in freezerEffectInvokers)
        {
            invoker.AddFreezerEffectListener(listener);
        }
    }

    public static void AddSpeedupEffectInvoker(PickupBlock invoker)
    {
        speedupEffectInvokers.Add(invoker);
        foreach (var listener in speedupEffectListeners)
        {
            invoker.AddSpeedupEffectListener(listener);
        }
    }

    public static void AddSpeedupEffectListener(UnityAction<float, float> listener)
    {
        speedupEffectListeners.Add(listener);
        foreach (var invoker in speedupEffectInvokers)
        {
            invoker.AddSpeedupEffectListener(listener);
        }
    }

    public static void AddPointsAddedInvoker(Block invoker)
    {
        pointsAddedInvokers.Add(invoker);
        foreach (var listener in pointsAddedListeners)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
    }

    public static void AddPointsAddedListener(UnityAction<int> listener)
    {
        pointsAddedListeners.Add(listener);
        foreach (var invoker in pointsAddedInvokers)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
    }

    public static void AddBallsReducedInvoker(Ball invoker)
    {
        ballsReducedInvokers.Add(invoker);
        foreach (var listener in ballsReducedListeners)
        {
            invoker.AddBallsReducedListener(listener);
        }
    }

    public static void AddBallsReducedListener(UnityAction listener)
    {
        ballsReducedListeners.Add(listener);
        foreach (var invoker in ballsReducedInvokers)
        {
            invoker.AddBallsReducedListener(listener);
        }
    }

    public static void AddBallDestroyedInvoker(Ball invoker)
    {
        ballDestroyedInvokers.Add(invoker);
        foreach (var listener in ballDestroyedListeners)
        {
            invoker.AddBallDestroyedListener(listener);
        }
    }

    public static void AddBallDestroyedListener(UnityAction listener)
    {
        ballDestroyedListeners.Add(listener);
        foreach (var invoker in ballDestroyedInvokers)
        {
            invoker.AddBallDestroyedListener(listener);
        }
    }
}
