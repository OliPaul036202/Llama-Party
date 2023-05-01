using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingBattleSystem : MonoBehaviour
{
    public enum BattleState { START, PLAYERONETURN, PLAYERTWOTURN, WIN, LOST }

    private BattleState battleState;

    //Get draw system so both player 1 and 2 can draw cards at the end of each of their turns.
    private DrawSystem drawSystem;

    //Get orb system to reset orbs back to max after turn
    private OrbSystem orbSystem;

    //Get score system to determine who won at the end of turn 7 - The final turn
    private ScoreSystem scoreSystem;

    //Keep track of which turn the game is currently on. // **NEED TO ADD MAX TURN WHEN GAMEPLAY IS FINISHED** //
    public int turnCounter = 0;

    //Get all player 1 current active cards
    [SerializeField]
    private BoardSystem boardSystem;

    //Audio
    public AudioClip bells;
    private AudioSource audioSource;

    public OpponentAgent opponentAgent; // **FOR REAL** //
    public OpponentAgent player1Agent; // **FOR REAL** //

    // Start is called before the first frame update
    void Start()
    {

        orbSystem = GetComponent<OrbSystem>();
        drawSystem = GetComponent<DrawSystem>();

        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();

        audioSource = GetComponent<AudioSource>();

        //Get boardsystem to keep track active cards on board
        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();
        battleState = BattleState.START;
        StartCoroutine(BeginBattle());
    }

    public IEnumerator BeginBattle()
    {
        Debug.Log("Starting Battle...");
        turnCounter += 1;
        audioSource.Play();
        yield return new WaitForSeconds(0.01f); //Wait 3 seconds then it is Player 1 Agents turn

        battleState = BattleState.PLAYERONETURN;
        yield return StartCoroutine(PlayersTurn());
    }

    IEnumerator PlayersTurn()
    {
        
        Debug.Log("Players 1s Turn");

        //player1Agent.RequestDecision();
        yield return new WaitForSeconds(0.01f);
        endPlayerTurn();
    }

    public void endPlayerTurn()
    {

        //Loop through all active player 1 cards
        for (int i = 0; i < boardSystem.availableBoardSlots.Length; i++)
        {
            //Check to see if a card has been played on this slot
            if (boardSystem.availableBoardSlots[i] == false)
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

        battleState = BattleState.PLAYERTWOTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Player 2s Turn");

        //Allow the Agent to play its turn.
        opponentAgent.RequestDecision();

        yield return new WaitForSeconds(0.01f);

        /*        basicAI.playCard();
                yield return new WaitForSeconds(0.5f);
                basicAI.playCard();
                yield return new WaitForSeconds(0.5f);
                basicAI.playCard();*/

        yield return new WaitForSeconds(1);
        turnCounter += 1;
        audioSource.Play();

        //Check to see if the game has reached the end of turn 7
        if (turnCounter == 8)
        {
            endGame();
        }
        else
        {
            endEnemyTurn();
        }

    }

    public void endEnemyTurn()
    {
        battleState = BattleState.PLAYERONETURN;
        StartCoroutine(PlayersTurn());
    }

    /// <summary>
    /// End game with a result of who won
    /// </summary>
    void endGame()
    {
        //Let the player know if they won or not at the end of the game by comparing player 1 and player 2 scores
        //Only the player needs to know visually
        if (scoreSystem.playerScore > scoreSystem.player2Score)
        {
            Debug.Log("PLAYER 1 WINS!");
        }
        else if (scoreSystem.playerScore < scoreSystem.player2Score)
        {
            Debug.Log("PLAYER 2 WINS!");
        }

        turnCounter = 0;
        StartCoroutine(PlayersTurn());
    }
}
