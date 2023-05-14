using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandPlayer1 : CardHand
{
    private bool OverPlayer1Side = true;
    public RectTransform player1SideTransform;

    void Start()
    {
        player1SideTransform = GameObject.Find("FriendlySide").GetComponent<RectTransform>();

        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();

        OrbSystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<OrbSystem>();
    }

    void Update()
    {
        if (rectOverlaps(player1SideTransform, this.GetComponent<RectTransform>()))
        {
            OverPlayer1Side = true;
        }
        else
        {
            OverPlayer1Side = false;
        }

        if (OverPlayer1Side)
        {
            if (orbCost <= OrbSystem.playerCurrentOrbs && boardSystem.isPlayerSideAvailable() == true)
            {
                //Take cost from player 1 orbs
                OrbSystem.applyOrbCost(orbCost);

                boardSystem.playCard(boardCard);
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
