using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOST}

    private BattleState battleState;
    private bool canPlayCards;

    //Get draw system so both player 1 and 2 can draw cards at the end of each of their turns.
    private DrawSystem drawSystem;

    //Get orb system to reset orbs back to max after turn
    private OrbSystem orbSystem;

    //Get score system to determine who won at the end of turn 7 - The final turn
    private ScoreSystem scoreSystem;

    //Keep track of which turn the game is currently on. // **NEED TO ADD MAX TURN WHEN GAMEPLAY IS FINISHED** //
    private int turnCounter = 0;
    public GameObject turnPanel;
    public Text turnText;

    public TMP_Text HUDTurnText;

    //Get all player 1 current active cards
    [SerializeField]
    private BoardSystem boardSystem;

    //Change button
    public Image buttonImage;
    public Sprite buttonUp;
    public Sprite buttonDown;
    public Text buttonUpText;
    public Text buttonDownText;

    //For tutorial & story
    public GameObject DialogueBoxCards;
    public GameObject DialogueBoxWin;
    private bool hasSeenTutorial = false;

    //Quit, Next Level buttons for when game ends
    public GameObject winPanelMenu;
    public GameObject losePanelMenu;

    //Audio
    public AudioClip bells;
    private AudioSource audioSource;

    public BasicAI_Controller basicAI; // **FOR TESTING** //

    //public OpponentAgent opponentAgent; // **FOR REAL** //

    // Start is called before the first frame update
    void Start()
    {

        turnPanel.SetActive(false);
        orbSystem = GetComponent<OrbSystem>();
        drawSystem = GetComponent<DrawSystem>();

        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();

        audioSource = GetComponent<AudioSource>();

        //Get boardsystem to keep track active cards on board
        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();
        buttonDownText.enabled = false;
        canPlayCards = false;
        battleState = BattleState.START;
    }

    public IEnumerator BeginBattle()
    {
        Debug.Log("Starting Battle...");
        turnCounter += 1;

        //Show Pop-Up UI to show what turn it is
        turnPanel.SetActive(true);
        turnText.text = "TURN: " + turnCounter.ToString();
        HUDTurnText.text = turnCounter.ToString();
        audioSource.Play();

        //Wait X seconds then start player 1s turn
        yield return new WaitForSeconds(3);
        battleState = BattleState.PLAYERTURN;
        turnPanel.SetActive(false);
        yield return StartCoroutine(PlayersTurn());
    }

    IEnumerator PlayersTurn()
    {
        //Reset Llama Orbs to max and draw up to three cards
        orbSystem.resetOrbs();
        drawSystem.DrawCards();
        drawSystem.DrawCards();
        drawSystem.DrawCards();

        //Wait X seconds then start the players turn.
        yield return new WaitForSeconds(1);
        Debug.Log("Players Turn");

        //If the player has not seen the tutorial, display it
        if (!hasSeenTutorial)
        {
            DialogueBoxCards.SetActive(true);
            hasSeenTutorial = true;
        }

        //Button UI come up ready to be pressed down by the player to end turn
        buttonImage.sprite = buttonUp;
        buttonUpText.enabled = true;
        buttonDownText.enabled = false;
        canPlayCards = true;
    }

    public void endPlayerTurn()
    {
        canPlayCards = false;
        buttonImage.sprite = buttonDown;
        buttonUpText.enabled = false;
        buttonDownText.enabled = true;

        //Loop through all active player 1 cards
        for(int i = 0; i < boardSystem.availableBoardSlots.Length; i++)
        {
            //Check to see if a card has been played on this slot
            if(boardSystem.availableBoardSlots[i] == false)
            {
                Debug.Log("Found active card");
                //Get child card from slot
                GameObject childCard = boardSystem.boardSlots[i].GetChild(0).gameObject;

                //Check to see if child card has headbutt script
                CardHeadbutt headButtCard = childCard.GetComponent<CardHeadbutt>();
                if (headButtCard)
                {
                    if (headButtCard.headbuttActive)
                    {
                        headButtCard.attackPlayer();
                        boardSystem.availableBoardSlots[i] = true;
                    }
                }
                else
                {
                    //Check to see if child card has fortify script
                    CardFortify fortifyCard = childCard.GetComponent<CardFortify>();
                    if (fortifyCard)
                    {
                        if (fortifyCard.fortifyActive)
                        {
                            fortifyCard.fortifyPlayer();
                            boardSystem.availableBoardSlots[i] = true;
                        }
                    }
                }
            }
        }

        battleState = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        //AI turn
        yield return new WaitForSeconds(1);
        Debug.Log("Enemy Turn");

        //Allow the Agent to play its turn.
        //opponentAgent.RequestDecision();

        basicAI.playCard();
        yield return new WaitForSeconds(0.5f);
        basicAI.playCard();
        yield return new WaitForSeconds(0.5f);
        basicAI.playCard();

        yield return new WaitForSeconds(1);
        turnCounter += 1;
        turnPanel.SetActive(true);
        turnText.text = "TURN: " + turnCounter.ToString();
        turnText.enabled = true;
        HUDTurnText.text = turnCounter.ToString();
        audioSource.Play();
        yield return new WaitForSeconds(3f);

        //Check to see if the game has reached the end of turn 7
        if(turnCounter == 8)
        {
            endGame();
        }else if(turnCounter < 8)
        {
            turnPanel.SetActive(false);
            endEnemyTurn();
        }
    }

    public void endEnemyTurn()
    {
        battleState = BattleState.PLAYERTURN;
        StartCoroutine(PlayersTurn());
    }

    /// <summary>
    /// End game with a result of who won
    /// </summary>
    void endGame()
    {
        //Let the player know if they won or not at the end of the game by comparing player 1 and player 2 scores
        //Only the player needs to know visually
        if(scoreSystem.playerScore > scoreSystem.player2Score)
        {
            turnPanel.SetActive(true);
            turnText.text = "WIN";
            turnText.enabled = true;
            DialogueBoxWin.SetActive(true);
            winPanelMenu.SetActive(true);
        }else if(scoreSystem.playerScore <= scoreSystem.player2Score)
        {
            turnPanel.SetActive(true);
            turnText.text = "LOST";
            turnText.enabled = true;
            losePanelMenu.SetActive(true);
        }
    }
}
