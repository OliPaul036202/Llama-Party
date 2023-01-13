using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broker_Ability : MonoBehaviour
{
    private OrbSystem orbSystem;
    void Start()
    {
        orbSystem = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent<OrbSystem>();

        orbSystem.playerCurrentOrbs += 1;
    }
}
