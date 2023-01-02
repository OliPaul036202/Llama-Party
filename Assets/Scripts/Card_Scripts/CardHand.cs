using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;
    private bool isPointerDown = false;
    private bool isHovering = false;
    private Vector3 cachedPos;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        transform.position = cachedPos;
        isPointerDown = false;
    }

    private void Update()
    {
        if (isPointerDown)
        {
            animator.enabled = false;
            Vector3 mousePos = Input.mousePosition;
            transform.position = mousePos;
        }else
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

}
