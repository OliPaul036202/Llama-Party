using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActive : MonoBehaviour
{
    public int llamaPoints;
    public int orbCost;

    public bool Defender;
    public bool Attacker;

    private ScoreSystem scoreSystem;

    void Start()
    {

        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();

        if (Defender)
        {
            scoreSystem.addPointsToPlayer(llamaPoints);
        }

        if (Attacker)
        {
            scoreSystem.takePointsFromOpponent(llamaPoints);
        }
    }
}
