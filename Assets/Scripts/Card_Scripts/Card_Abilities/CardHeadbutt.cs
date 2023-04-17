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

    //So Battle System can know which cards are activated or not
    public bool headbuttActive = false;

    //Check if this card belongs to player 1 or player 2 and how many llama points this card has
    private CardActive cardActive;

    //Get score system so that the relavant amount of llama points are removed from opposing player
    public ScoreSystem scoreSystem;

    //Get enemy portrait for charge
    private Transform enemyPortraitPos;
    public float chargeSpeed = 50f;
    private bool canCharge = false;

    void Start()
    {
        checkBoxSpriteRend.enabled = false;
        //Get card active script on this game object
        cardActive = GetComponent<CardActive>();

        //Find score system script on different game object
        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();

        enemyPortraitPos = GameObject.FindGameObjectWithTag("Player2Portrait").transform;
    }

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
            headbuttActive = false;
        }
        else if (headbuttToggle)
        {
            checkBoxSpriteRend.enabled = true;
            headbuttActive = true;
        }

        if (canCharge)
        {
            float step = chargeSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, enemyPortraitPos.position, step);
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

    /// <summary>
    /// Called by the battle system in the battle phase. Is executed when the player activates the Headbutt ability on this card.
    /// </summary>
    public void attackPlayer()
    {
        //Check to see if the headbutt ability has been activated for this card
        if (headbuttActive)
        {
            //Check which player this card belongs to
            if (cardActive.player1BoardCard)
            {
                //Take llama points away from player 2 if this card belongs to player 1
                scoreSystem.player2Score -= cardActive.llamaPoints;
            }
            else if (cardActive.player2BoardCard)
            {
                //Take llama points away from player 1 if this card belongs to player 2
                scoreSystem.playerScore -= cardActive.llamaPoints;
            }
        }
        //Charge at enemy portrait and HEADBUTT
        transform.position = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);
        canCharge = true;
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy this gameobject on collision with player 2 portrait
        if (other.gameObject.tag == "Player2Portrait")
        {
            Debug.Log("Collided");
            Destroy(gameObject);
        }
    }
}
