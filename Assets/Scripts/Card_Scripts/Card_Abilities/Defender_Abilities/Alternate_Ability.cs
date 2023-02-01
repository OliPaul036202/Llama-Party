using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alternate_Ability : MonoBehaviour
{
    public GameObject[] activeDefenders;

    public GameObject alternatePrefab;

    // Start is called before the first frame update
    void Start()
    {
        activeDefenders = GameObject.FindGameObjectsWithTag("ActiveDefender");

        int randDef = Random.Range(0, activeDefenders.Length);

        Instantiate(alternatePrefab, new Vector3(activeDefenders[randDef].transform.position.x,
                                      activeDefenders[randDef].transform.position.y,
                                      activeDefenders[randDef].transform.position.z
                                      ), Quaternion.identity);

        Destroy(activeDefenders[randDef]);

        //scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();
        //scoreSystem.addPointsToPlayer(2);
    }
}
