using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broker_Ability : MonoBehaviour
{
    private OrbSystem orbSystem;
    void Start()
    {
        orbSystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<OrbSystem>();

        if (orbSystem)
        {
            Debug.Log("Broker Ability activated");
            orbSystem.playerCurrentOrbs += 1;
        }
    }
}
