using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alternate_Ability : MonoBehaviour
{
    public AbilityManager abilityManager;
    // Start is called before the first frame update
    void Start()
    {
        abilityManager = GameObject.FindGameObjectWithTag("AbilityManager").GetComponent<AbilityManager>();
        abilityManager.isAlternateActive = true;
    }
}
