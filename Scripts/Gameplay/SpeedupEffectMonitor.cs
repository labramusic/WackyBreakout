using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// keeps track of the effect's status for the whole game
public class SpeedupEffectMonitor : MonoBehaviour
{
    // timer to keep track of whether the effect is active
    private Timer SpeedupTimer;

    public bool SpeedupActive { get; set; }
    public float SpeedupFactor { get; set; }
    public float RemainingEffectTime
    {
        get { return SpeedupTimer.TimeRemaining; }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpeedupTimer = gameObject.AddComponent<Timer>();
        EventManager.AddSpeedupEffectListener(HandleSpeedupEffectActivatedEvent);
        SpeedupTimer.AddTimerFinishedListener(SpeedupTimerFinished);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SpeedupTimerFinished()
    {
        SpeedupActive = false;
        SpeedupTimer.Stop();
    }

    private void HandleSpeedupEffectActivatedEvent(float effectDuration, float ballSpeedupFactor)
    {
        if (SpeedupTimer.Running)
        {
            SpeedupTimer.ExtendDuration(effectDuration);
        }
        else
        {
            SpeedupTimer.Duration = effectDuration;
            SpeedupTimer.Run();

            SpeedupActive = true;
            SpeedupFactor = ballSpeedupFactor;
        }
    }
}
