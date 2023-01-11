using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActive : MonoBehaviour
{
    public int llamaPoints;
    public int orbCost;

    private ScoreSystem scoreSystem;
    void Start()
    {
        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();

        scoreSystem.addPointsToPlayer(llamaPoints);
    }
}
