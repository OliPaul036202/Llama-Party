using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public int playerScore;
    public int player2Score;

    public TMP_Text playerScoreText;
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
    }

    public void addPointsToPlayer(int LlamaPoints)
    {
        playerScore += LlamaPoints;
    }
}
