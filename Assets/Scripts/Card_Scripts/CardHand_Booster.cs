using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script for Booster cards when they are in the players hand.
/// </summary>
public class CardHand_Booster : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    //Animator for in hand hover over animations
    private Animator animator;
    private bool isPointerDown = false;
    private bool isHovering = false;

    //Stash position of hand so the card can return to it
    private Vector3 cachedPos;

    //Check to see if the card is over the players side of the board or not
    private RectTransform uiRect1;
    private bool OverPlayer1Side = false;

    //Get Game Systems manager to access various systems
    public GameObject GameSystemsManager;
    private GameObject boardCard;
    private OrbSystem OrbSystem;
    private DrawSystem drawSystem;

    public BoardSystem boardSystem;

    //Cost of card
    [SerializeField]
    private int orbCost;

    void Start()
    {
        //Get animator for hand animations
        animator = GetComponent<Animator>();

        //Find the 'friendlyside' of the board
        uiRect1 = GameObject.Find("FriendlySide").GetComponent<RectTransform>();

        //Find Game Systems Manager and all required attached scripts
        GameSystemsManager = GameObject.FindGameObjectWithTag("SystemsManager");
        OrbSystem = GameSystemsManager.GetComponent<OrbSystem>();
        drawSystem = GameSystemsManager.GetComponent<DrawSystem>();

        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();

        //Set orb cost
        orbCost = GetComponent<CardDisplay>().card.orbCost;
    }

    void Update()
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

    /// <summary>
    /// Tracks to see if players cursor is over this card or not
    /// </summary>
    /// <param name="eventData">Mouse input</param>
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

    public void OnPointerUp(PointerEventData eventData)
    {
        return;
    }


    bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }
}
