using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    public static float HalfColliderWidth;
    public static float HalfColliderHeight;

    private Timer StartDelayTimer;
    private Timer LifetimeTimer;
    private BallSpawner BallSpawner;
    private Timer SpeedupTimer;

    private BallsReducedEvent BallsReducedEvent;
    private BallDestroyedEvent BallDestroyedEvent;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        HalfColliderWidth = (GetComponent<BoxCollider2D>().size.x * transform.localScale.x) / 2;
        HalfColliderHeight = (GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2;

        BallSpawner = Camera.main.GetComponent<BallSpawner>();

        StartDelayTimer = gameObject.AddComponent<Timer>();
        StartDelayTimer.AddTimerFinishedListener(StartDelayTimerFinished);
        StartDelayTimer.Duration = ConfigurationUtils.BallStartDelay;
        StartDelayTimer.Run();

        LifetimeTimer = gameObject.AddComponent<Timer>();
        LifetimeTimer.AddTimerFinishedListener(LifetimeTimerFinished);

        SpeedupTimer = gameObject.AddComponent<Timer>();
        SpeedupTimer.AddTimerFinishedListener(SpeedupTimerFinished);
        EventManager.AddSpeedupEffectListener(HandleSpeedupEffectActivatedEvent);

        BallsReducedEvent = new BallsReducedEvent();
        BallDestroyedEvent = new BallDestroyedEvent();
        EventManager.AddBallsReducedInvoker(this);
        EventManager.AddBallDestroyedInvoker(this);

        // prevent balls colliding with other balls
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void StartDelayTimerFinished()
    {
        StartMoving();
        LifetimeTimer.Duration = ConfigurationUtils.BallLifetime;
        LifetimeTimer.Run();
    }

    private void LifetimeTimerFinished()
    {
        Destroy(gameObject);
        BallDestroyedEvent.Invoke();
    }

    private void SpeedupTimerFinished()
    {
        Rigidbody.velocity = Rigidbody.velocity.magnitude / EffectUtils.SpeedupFactor * Rigidbody.velocity.normalized;
        SpeedupTimer.Stop();
    }

    private void StartMoving()
    {
        if (EffectUtils.SpeedupActive)
        {
            Rigidbody.AddForce(ConfigurationUtils.BallImpulseForce * EffectUtils.SpeedupFactor * new Vector2(0, -1));
            SpeedupTimer.Duration = EffectUtils.RemainingEffectTime;
            SpeedupTimer.Run();
        }
        else
        {
            //var rot = Quaternion.AngleAxis(-90, Vector3.forward);
            //var lDirection = rot * Vector3.right;
            //var wDirection = transform.TransformDirection(lDirection);
            //Rigidbody.AddForce(ConfigurationUtils.BallImpulseForce * lDirection);
            Rigidbody.AddForce(ConfigurationUtils.BallImpulseForce * new Vector2(0, -1)); // cos sin -90
        }
    }

    public void SetDirection(Vector2 direction)
    {
        //we typically add forces to change velocity but it will be easier if we change the velocity of the rigidbody attached to the ball directly
        Rigidbody.velocity = Rigidbody.velocity.magnitude * direction;
    }

    private void OnBecameInvisible()
    {
        // the death timer and game shutdown also make the ball invisible
        // still known to be called sometime on game shutdown!
        if (transform.position.y + HalfColliderHeight < ScreenUtils.ScreenBottom)
        {
            Destroy(gameObject);
            BallsReducedEvent.Invoke();
        }
    }

    // moguce jedan timer??
    private void HandleSpeedupEffectActivatedEvent(float effectDuration, float ballSpeedupFactor)
    {
        if (SpeedupTimer.Running)
        {
            SpeedupTimer.ExtendDuration(effectDuration);
        }
        else
        {
            Rigidbody.velocity = Rigidbody.velocity.magnitude * ballSpeedupFactor * Rigidbody.velocity.normalized;
            SpeedupTimer.Duration = effectDuration;
            SpeedupTimer.Run();
        }
    }

    public void AddBallsReducedListener(UnityAction listener)
    {
        BallsReducedEvent.AddListener(listener);
    }

    public void AddBallDestroyedListener(UnityAction listener)
    {
        BallDestroyedEvent.AddListener(listener);
    }
}
