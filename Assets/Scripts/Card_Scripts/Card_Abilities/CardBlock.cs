using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBlock : MonoBehaviour
{
    //Get check box sprite for visual representation of activating block
    public SpriteRenderer checkBoxSpriteRend;

    //Toggle sprite and block ability
    private bool blockToggle = false;

    //Set delay so the mouse doesn't 'activate' block instantly when played
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
        if(delay <= 0)
        {
            canActivate = true;
        }

        //Toggle block activation
        if (!blockToggle)
        {
            checkBoxSpriteRend.enabled = false;
        } else if (blockToggle)
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
                if (!blockToggle)
                {
                    blockToggle = true;
                }
                else if (blockToggle)
                {
                    blockToggle = false;
                }
            }
        }
    }
}
