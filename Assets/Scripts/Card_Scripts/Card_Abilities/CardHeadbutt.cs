using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHeadbutt : MonoBehaviour
{
    //Get check box sprite for visual representation of activating headbutt
    public SpriteRenderer checkBoxSpriteRend;

    //Toggle sprite and headbutt ability
    private bool headbuttToggle = false;

    //Set delay so the mouse doesn't 'activate' headbutt instantly when played
    private float delay = 1.5f;
    private bool canActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        checkBoxSpriteRend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            canActivate = true;
        }

        //Toggle block activation
        if (!headbuttToggle)
        {
            checkBoxSpriteRend.enabled = false;
        }
        else if (headbuttToggle)
        {
            checkBoxSpriteRend.enabled = true;
        }
    }

    private void OnMouseOver()
    {
        if (canActivate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!headbuttToggle)
                {
                    headbuttToggle = true;
                }
                else if (headbuttToggle)
                {
                    headbuttToggle = false;
                }
            }
        }
    }
}
