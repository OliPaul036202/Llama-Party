using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFortify : MonoBehaviour
{
    //Get check box sprite for visual representation of activating headbutt
    public SpriteRenderer checkBoxSpriteRend;

    //Toggle sprite and headbutt ability
    private bool fortifyToggle = false;

    //Set delay so the mouse doesn't 'activate' headbutt instantly when played
    private float delay = 1f;
    private bool canActivate = false;

    //So Battle System can know which cards are activated or not
    public bool fortifyActive = false;

    //Check if this card belongs to player 1 or player 2 and how many llama points this card has
    private CardActive cardActive;

    //Get score system so that the relavant amount of llama points are removed from opposing player
    public ScoreSystem scoreSystem;

    //Get the orb system to take away orbs when toggling ability.
    [SerializeField] public OrbSystem orbSystem;

    public GameObject effectPrefab;

    void Start()
    {
        checkBoxSpriteRend.enabled = false;
        //Get card active script on this game object
        cardActive = GetComponent<CardActive>();

        //Find score system script on different game object
        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();

        orbSystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<OrbSystem>();
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            canActivate = true;
        }

        //Toggle block activation
        if (!fortifyToggle)
        {
            checkBoxSpriteRend.enabled = false;
            fortifyActive = false;
        }
        else if (fortifyToggle)
        {
            checkBoxSpriteRend.enabled = true;
            fortifyActive = true;
        }
    }

    private void OnMouseOver()
    {
        if (canActivate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!fortifyToggle && orbSystem.playerCurrentOrbs >= cardActive.orbCost)
                {
                    fortifyToggle = true;
                    orbSystem.playerCurrentOrbs -= cardActive.orbCost;
                }
                else if (fortifyToggle)
                {
                    fortifyToggle = false;
                    orbSystem.playerCurrentOrbs += cardActive.orbCost;
                }
            }
        }
    }

    /// <summary>
    /// Called by the battle system in the battle phase. Is executed when the player activates the FORTIFY ability on this card.
    /// </summary>
    public void fortifyPlayer()
    {
        Debug.Log("FORTIFYING PLAYER...");
        //Check to see if the headbutt ability has been activated for this card
        if (fortifyActive)
        {
            //Check which player this card belongs to
            if (cardActive.player1BoardCard)
            {
                //Give points to player 1 if it is a player 1 card
                scoreSystem.playerScore += cardActive.llamaPoints;
            }
            else if (cardActive.player2BoardCard)
            {
                //Give points to player 2 if it is a player 2 card
                scoreSystem.player2Score += cardActive.llamaPoints;
            }
        }
        Instantiate(effectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
