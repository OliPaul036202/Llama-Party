using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHandPlayer2 : CardHand
{
    private bool OverPlayer2Side = true;
    public RectTransform player2SideTransform;
    public GameObject boardCard2;

    private void Start()
    {
        player2SideTransform = GameObject.Find("Player2Side").GetComponent<RectTransform>();

        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();

        OrbSystem = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent<OrbSystem>();
    }

    private void Update()
    {
        if (rectOverlaps(player2SideTransform, this.GetComponent<RectTransform>()))
        {
            OverPlayer2Side = true;
        }
        else
        {
            OverPlayer2Side = false;
        }

        if (OverPlayer2Side)
        {
            if (orbCost <= OrbSystem.player2CurrentOrbs && boardSystem.isPlayer2SideAvailable() == true)
            {
                //Take cost from playe 2 orbs
                OrbSystem.applyOrbCostPlayer2(orbCost);
                //Play card on player 2 side
                boardSystem.player2PlayCard(boardCard2);
                gameObject.SetActive(false);
            }
        }
    }

    bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }
}    
