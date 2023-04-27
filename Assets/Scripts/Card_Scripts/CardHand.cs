using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public GameObject GameSystemsManager;

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

        //Find Game Systems Manager and all required attached scripts
        GameSystemsManager = GameObject.FindGameObjectWithTag("SystemsManager");
        OrbSystem = GameSystemsManager.GetComponent<OrbSystem>();
        drawSystem = GameSystemsManager.GetComponent<DrawSystem>();

        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();

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
        uiRect1.GetComponent<Image>().color = new Color(4f, 255f, 78f, 0.40f);
    }

    public void OnPointerUp(PointerEventData data)
    {
        // Check to see if player has tried to play a card over player 1s side
        if (OverPlayer1Side)
        {
            // If the hand card is a player 1 card then play card
            if (player1Card)
            {
                if (orbCost <= OrbSystem.playerCurrentOrbs && boardSystem.isPlayerSideAvailable() == true)
                {
                    uiRect1.GetComponent<Image>().color = new Color(4f, 255f, 78f, 0f);
                    //Take cost from players orbs
                    OrbSystem.applyOrbCost(orbCost);
                    //Play board card
                    boardSystem.playCard(boardCard);
                    drawSystem.availableCardSlots[handIndex] = true;
                    //Discard hand card
                    gameObject.SetActive(false);
                }
                else
                {
                    // If the player cannot afford to play this card; return it to their hand
                    transform.position = cachedPos;
                    isPointerDown = false;
                    uiRect1.GetComponent<Image>().color = new Color(4f, 255f, 78f, 0f);
                }
            }
        } else if (!OverPlayer1Side && overPlayer2Side)
        {
            // If the player tried to play outside the friendly area
            transform.position = cachedPos;
            isPointerDown = false;
            uiRect1.GetComponent<Image>().color = new Color(4f, 255f, 78f, 0f);
        }
        
        if (overPlayer2Side)
        {
            if (player2Card)
            {
                Debug.Log("Over Enemy Side");
                //Check to see if the player 2 AI can afford to play this card
                if(orbCost <= OrbSystem.player2CurrentOrbs && boardSystem.isPlayer2SideAvailable() == true)
                {
                    //Take cost from players orbs
                    OrbSystem.applyOrbCost(orbCost);
                    //Play card
                    boardSystem.player2PlayCard(boardCard);
                    drawSystem.availableCardSlots[handIndex] = true;
                    gameObject.SetActive(false);
                }
                else
                {
                    // If the player cannot afford to play this card; return it to their hand
                    transform.position = cachedPos;
                    isPointerDown = false;
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            // If the player cannot afford to play this card; return it to their hand
            transform.position = cachedPos;
            isPointerDown = false;
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
            if (animator)
            {
                animator.enabled = true;
                animator.SetBool("OnHover", false);
            }
        }

        if (isHovering)
        {
            if (animator)
            {
                animator.SetBool("OnHover", true);
            }
        }
        else
        {
            if (animator)
            {
                animator.SetBool("OnHover", false);
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
