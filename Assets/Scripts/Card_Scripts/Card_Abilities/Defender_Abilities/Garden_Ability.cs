using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden_Ability : MonoBehaviour
{
    public GameObject[] activeDefenders;

    private ScoreSystem scoreSystem;
    // Start is called before the first frame update
    void Start()
    {
        activeDefenders = GameObject.FindGameObjectsWithTag("ActiveDefender");

        int randDef = Random.Range(0, activeDefenders.Length);

        activeDefenders[randDef].GetComponent<CardActive>().llamaPoints += 1;

        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();
        scoreSystem.addPointsToPlayer(1);
    }
}
