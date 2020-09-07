using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private static Text ScoreText;
    private static int Score;
    private static Text BallsLeftText;
    private static int BallsLeft;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        BallsLeft = ConfigurationUtils.BallsPerGame;
        ScoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
        ScoreText.text = "Score: " + Score;
        BallsLeftText = GameObject.FindWithTag("BallsLeft").GetComponent<Text>();
        BallsLeftText.text = "Balls left: " + BallsLeft;

        EventManager.AddPointsAddedListener(AddPointsToScore);
        EventManager.AddBallsReducedListener(BallLost);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddPointsToScore(int points)
    {
        Score += points;
        ScoreText.text = "Score: " + Score;
    }

    private void BallLost()
    {
        BallsLeft -= 1;
        BallsLeftText.text = "Balls left: " + BallsLeft;
    }
}
