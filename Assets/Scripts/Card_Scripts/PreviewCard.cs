using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCard : MonoBehaviour
{
    public Image cardBoarder;

    public Sprite defenderBoarder;
    public Sprite attackerBoarder;

    public void setDefenderBoarder()
    {
        cardBoarder.sprite = defenderBoarder;
    }

    public void setAttackerBoarder()
    {
        cardBoarder.sprite = attackerBoarder;
    }
}
