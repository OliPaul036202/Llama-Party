using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;
    private bool isPointerDown = false;
    private bool isHovering = false;

    private bool isOverFriendlySide = false;

    private Vector3 cachedPos;

    private RectTransform uiRect1;

    public GameObject boardCard;
    public BoardSystem boardSystem;

    private void Start()
    {
        animator = GetComponent<Animator>();

        uiRect1 = GameObject.Find("FriendlySide").GetComponent<RectTransform>();

        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();
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
        if (isOverFriendlySide)
        {
            boardSystem.playCard(boardCard);
            gameObject.SetActive(false);
        }
        else if (!isOverFriendlySide)
        {
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
                isOverFriendlySide = true;
            }
            else
            {
                isOverFriendlySide = false;
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
