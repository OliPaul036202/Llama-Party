using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;
    private bool isPointerDown = false;
    private bool isHovering = false;

    private bool OverPlayer1Side = false;

    private Vector3 cachedPos;

    private RectTransform uiRect1;

    private RectTransform player2SideTransform;
    private bool overPlayer2Side = false;

    public GameObject boardCard;
    public BoardSystem boardSystem;

    public OrbSystem OrbSystem;
    public int orbCost;

    public int handIndex;
    public DrawSystem drawSystem;

    public bool player1Card;
    public bool player2Card;

    private void Start()
    {
        animator = GetComponent<Animator>();

        uiRect1 = GameObject.Find("FriendlySide").GetComponent<RectTransform>();

        player2SideTransform = GameObject.Find("Player2Side").GetComponent<RectTransform>();

        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();

        drawSystem = GameObject.FindGameObjectWithTag("DrawSystem").GetComponent<DrawSystem>();

        OrbSystem = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent<OrbSystem>();

        orbCost = GetComponent<CardDisplay>().card.orbCost;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cachedPos = transform.position;
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        // Check to see if player has tried to play a card over player 1s side
        if (OverPlayer1Side)
        {
            // If the card is a player 1 card then play card
            if (player1Card)
            {
                if (orbCost <= OrbSystem.playerCurrentOrbs && boardSystem.isPlayerSideAvailable() == true)
                {
                    //Take cost from players orbs
                    OrbSystem.applyOrbCost(orbCost);
                    //Play card
                    boardCard.GetComponent<CardActive>().player2Card = false;
                    boardCard.GetComponent<CardActive>().player1Card = true;
                    boardSystem.playCard(boardCard);
                    drawSystem.availableCardSlots[handIndex] = true;
                    gameObject.SetActive(false);
                }
            }
        }else if (overPlayer2Side)
        {
            if (player2Card)
            {
                //Check to see if the player 2 AI can afford to play this card
                if(orbCost <= OrbSystem.player2CurrentOrbs && boardSystem.isPlayer2SideAvailable() == true)
                {
                    //Take cost from players orbs
                    OrbSystem.applyOrbCost(orbCost);
                    //Play card
                    boardCard.GetComponent<CardActive>().player2Card = true;
                    boardCard.GetComponent<CardActive>().player1Card = false;
                    boardSystem.playCard(boardCard);
                    drawSystem.availableCardSlots[handIndex] = true;
                    gameObject.SetActive(false);
                }
            }
        }
        else if(!OverPlayer1Side && OverPlayer1Side)
        {
            if (player1Card)
            {
                // If the player cannot afford to play this card; return it to their hand
                transform.position = cachedPos;
                isPointerDown = false;
            }
        }
    }

    private void Update()
    {
        if (isPointerDown)
        {
            animator.enabled = false;
            Vector3 mousePos = Input.mousePosition;
            transform.position = mousePos;

            //Check to see if card is over friendly side
            if (rectOverlaps(uiRect1, this.GetComponent<RectTransform>()))
            {
                OverPlayer1Side = true;
            }
            else
            {
                OverPlayer1Side = false;
            }

            if(rectOverlaps(player2SideTransform, this.GetComponent<RectTransform>()))
            {
                overPlayer2Side = true;
                Debug.Log("over player 2 side");
            }
            else
            {
                overPlayer2Side = false;
            }
        }
        else
        {
            animator.enabled = true;
            animator.SetBool("OnHover", false);
        }

        if (isHovering)
        {
            animator.SetBool("OnHover", true);
        }
        else
        {
            animator.SetBool("OnHover", false);
        }
    }

    bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }
}
