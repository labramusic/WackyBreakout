using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 
public class Paddle : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    private float halfColliderWidth;
    private float halfColliderHeight;
    private const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

    private bool frozen;
    private Timer FrozenTimer;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        halfColliderWidth = (GetComponent<BoxCollider2D>().size.x * transform.localScale.x) / 2;
        halfColliderHeight = (GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2;

        FrozenTimer = GetComponent<Timer>();
        EventManager.AddFreezerEffectListener(HandleFreezerEffectActivatedEvent);
        FrozenTimer.AddTimerFinishedListener(FrozenTimerFinished);
    }

    private void FixedUpdate() {
        if (!frozen)
        {
            var move = Input.GetAxis("Horizontal") * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.fixedDeltaTime;
            //transform.Translate(move, 0, 0);
            var newPosX = CalculateClampedX(Rigidbody.position.x + move);
            Rigidbody.MovePosition(new Vector2(newPosX, Rigidbody.position.y));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FrozenTimerFinished()
    {
        frozen = false;
        FrozenTimer.Stop();
    }

    private float CalculateClampedX(float xPos)
    {
        if (xPos < ScreenUtils.ScreenLeft + halfColliderWidth)
        {
            xPos = ScreenUtils.ScreenLeft + halfColliderWidth;
        }
        else if (xPos > ScreenUtils.ScreenRight - halfColliderWidth)
        {
            xPos = ScreenUtils.ScreenRight - halfColliderWidth;
        }
        return xPos;
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball") && IsTopCollision(coll))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x - coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter / halfColliderWidth;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
      
            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

    private bool IsTopCollision(Collision2D coll)
    {
        // collided ball bottom equal to paddle top within tolerance
        return Mathf.Abs((coll.transform.position.y - Ball.HalfColliderHeight) - (transform.position.y + halfColliderHeight)) <= 0.09f;
    }

    private void HandleFreezerEffectActivatedEvent(float effectDuration)
    {
        frozen = true;
        if (FrozenTimer.Running)
        {
            FrozenTimer.ExtendDuration(effectDuration);
        }
        else
        {
            FrozenTimer.Duration = effectDuration;
            FrozenTimer.Run();
        }
    }
}
