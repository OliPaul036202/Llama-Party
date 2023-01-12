using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrbSystem : MonoBehaviour
{
    private int playerMaxOrbs;
    public int playerCurrentOrbs;

    public TMP_Text player1OrbText;
    void Start()
    {
        playerMaxOrbs = 3;
        playerCurrentOrbs = playerMaxOrbs;
    }

    // Update is called once per frame
    void Update()
    {
        player1OrbText.text = playerCurrentOrbs.ToString() + "/3";
    }

    public void resetOrbs()
    {
        playerCurrentOrbs = playerMaxOrbs;
    }

    public void applyOrbCost(int cardCost)
    {
        playerCurrentOrbs -= cardCost;
    }

    public bool isOrbsZero(int currentOrbs)
    {
        if(currentOrbs <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
