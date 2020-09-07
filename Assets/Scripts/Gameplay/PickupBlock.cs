using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;

public class PickupBlock : Block
{
    [SerializeField]
    private Sprite freezerBlockSprite;

    [SerializeField]
    private Sprite speedupBlockSprite;


    [SerializeField, HideInInspector]
    private PickupEffect pickupEffect;
    private float effectDuration;

    private FreezerEffectActivated FreezerEvent;
    private SpeedupEffectActivated SpeedupEvent;

    public PickupEffect PickupEffect
    {
        set { 
            pickupEffect = value;
            switch(value)
            {
                case PickupEffect.Freezer:
                    GetComponent<SpriteRenderer>().sprite = freezerBlockSprite;
                    effectDuration = ConfigurationUtils.FreezeDuration;
                    FreezerEvent = new FreezerEffectActivated();
                    EventManager.AddFreezerEffectInvoker(this);
                    break;
                case PickupEffect.Speedup:
                    GetComponent<SpriteRenderer>().sprite = speedupBlockSprite;
                    effectDuration = ConfigurationUtils.SpeedupDuration;
                    SpeedupEvent = new SpeedupEffectActivated();
                    EventManager.AddSpeedupEffectInvoker(this);
                    break;
            }
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Points = ConfigurationUtils.PickupBlockPoints;
        // reserialize fields
        PickupEffect = pickupEffect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddFreezerEffectListener(UnityAction<float> listener)
    {
        FreezerEvent.AddListener(listener);
    }

    public void AddSpeedupEffectListener(UnityAction<float, float> listener)
    {
        SpeedupEvent.AddListener(listener);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (pickupEffect.Equals(PickupEffect.Freezer))
        {
            FreezerEvent.Invoke(effectDuration);
        }
        else if (pickupEffect.Equals(PickupEffect.Speedup))
        {
            SpeedupEvent.Invoke(effectDuration, ConfigurationUtils.BallSpeedupFactor);
        }

        base.OnCollisionEnter2D(collision);
    }
}
