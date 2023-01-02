using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Animator animator;
    private bool isPointerDown = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("OnHover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("OnHover", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");

        isPointerDown = true;
    }

    private void Update()
    {
        if (isPointerDown)
        {
            animator.enabled = false;
            Vector3 mousePos = Input.mousePosition;
            transform.position = mousePos;
        }
    }

}
