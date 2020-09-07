using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject BallPrefab;

    private Timer SpawnTimer;
    private bool RetrySpawn = false;
    private Vector2 SpawnLocationMin;
    private Vector2 SpawnLocationMax;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = gameObject.AddComponent<Timer>();
        StartDelayTimer();

        // SpawnBall
        GameObject firstBall = Instantiate(BallPrefab);
        // save lower left and upper right corners
        SpawnLocationMin = new Vector2(
            firstBall.transform.position.x - Ball.HalfColliderWidth,
            firstBall.transform.position.y - Ball.HalfColliderHeight);
        SpawnLocationMax = new Vector2(
            firstBall.transform.position.x + Ball.HalfColliderWidth,
            firstBall.transform.position.y + Ball.HalfColliderHeight);

        SpawnTimer.AddTimerFinishedListener(SpawnTimerFinished);

        EventManager.AddBallsReducedListener(SpawnBall);
        EventManager.AddBallDestroyedListener(SpawnBall);
    }

    // Update is called once per frame
    void Update()
    {
        if (RetrySpawn)
        {
            SpawnBall();
        }
    }

    public void SpawnTimerFinished()
    {
        SpawnBall();
        StartDelayTimer();
    }

    private void SpawnBall()
    {
        // make sure we don't spawn into a collision, try again next frame
        if (Physics2D.OverlapArea(SpawnLocationMin, SpawnLocationMax) == null)
        {
            RetrySpawn = false;
            Instantiate(BallPrefab);
        }
        else
        {
            RetrySpawn = true;
        }
    }

    private void StartDelayTimer()
    {
        SpawnTimer.Duration = Random.Range(ConfigurationUtils.MinSpawnTime, ConfigurationUtils.MaxSpawnTime);
        SpawnTimer.Run();
    }
}
