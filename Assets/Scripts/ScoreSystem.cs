using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public int playerScore;
    public int player2Score;

    public TMP_Text playerScoreText;
    public TMP_Text player2ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        player2Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = playerScore.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    public void addPointsToPlayer(int LlamaPoints)
    {
        playerScore += LlamaPoints;
    }

    public void takePointsFromPlayer(int LlamaPoints)
    {
        playerScore -= LlamaPoints;
    }

    public void takePointsFromPlayerTwo(int LlamaPoints)
    {
        player2Score -= LlamaPoints;
    }

    public void addPointsToPlayerTwo(int LlamaPoints)
    {
        player2Score += LlamaPoints;
    }
}
