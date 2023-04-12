using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrbSystem : MonoBehaviour
{
    private int playerMaxOrbs;
    public int playerCurrentOrbs;
    public TMP_Text player1OrbText;
    public Image orb1;
    public Image orb2;
    public Image orb3;
    public Sprite purpleOrb;
    public Sprite whiteOrb;

    private int player2MaxOrbs;
    public int player2CurrentOrbs;
    public TMP_Text player2OrbText;
    void Start()
    {
        playerMaxOrbs = 3;
        playerCurrentOrbs = playerMaxOrbs;

        player2MaxOrbs = 3;
        player2CurrentOrbs = player2MaxOrbs;

        orb1.sprite = purpleOrb;
        orb2.sprite = purpleOrb;
        orb3.sprite = purpleOrb;
    }

    // Update is called once per frame
    void Update()
    {
        player1OrbText.text = playerCurrentOrbs.ToString() + "/3";

        player2OrbText.text = player2CurrentOrbs.ToString() + "/3";

        if (playerCurrentOrbs == 3)
        {
            orb1.sprite = purpleOrb;
            orb2.sprite = purpleOrb;
            orb3.sprite = purpleOrb;
        } else if (playerCurrentOrbs == 2)
        {
            orb1.sprite = whiteOrb;
        } else if(playerCurrentOrbs == 1)
        {
            orb1.sprite = whiteOrb;
            orb2.sprite = whiteOrb;
        } else if(playerCurrentOrbs == 0)
        {
            orb1.sprite = whiteOrb;
            orb2.sprite = whiteOrb;
            orb3.sprite = whiteOrb;
        }
    }

    public void resetOrbs()
    {
        playerCurrentOrbs = playerMaxOrbs;
        player2CurrentOrbs = player2MaxOrbs;
    }

    public void applyOrbCost(int cardCost)
    {
        playerCurrentOrbs -= cardCost;
    }

    public void applyOrbCostPlayer2(int cardCost)
    {
        player2CurrentOrbs -= cardCost;
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
