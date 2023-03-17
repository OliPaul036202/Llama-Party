using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, BATTLE, WIN, LOST}

    private BattleState battleState;
    private bool canPlayCards;

    //Get draw system so both player 1 and 2 can draw cards at the end of each of their turns.
    private DrawSystem drawSystem;

    //Get orb system to reset orbs back to max after turn
    private OrbSystem orbSystem;

    //Keep track of which turn the game is currently on. // **NEED TO ADD MAX TURN WHEN GAMEPLAY IS FINISHED** //
    private int turnCounter = 0;
    public Text turnText;

    //Get all player 1 current active cards
    [SerializeField]
    private BoardSystem boardSystem;

    public BasicAI_Controller basicAI; // **FOR TESTING** //
    // Start is called before the first frame update
    void Start()
    {
        turnText.enabled = false;
        orbSystem = GetComponent<OrbSystem>();
        drawSystem = GetComponent<DrawSystem>();

        //Get boardsystem to keep track active cards on board
        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();

        canPlayCards = false;
        battleState = BattleState.START;
        StartCoroutine(BeginBattle());
    }

    IEnumerator BeginBattle()
    {
        Debug.Log("Starting Battle...");
        turnCounter += 1;
        turnText.enabled = true;
        turnText.text = "TURN: " + turnCounter.ToString();
        yield return new WaitForSeconds(2);
        //Wait 2 seconds then it is the players turn.
        battleState = BattleState.PLAYERTURN;
        turnText.enabled = false;
        yield return StartCoroutine(PlayersTurn());
    }

    IEnumerator PlayersTurn()
    {
        //Display message here stating its the players turn.
        orbSystem.resetOrbs();
        drawSystem.DrawCards();
        drawSystem.DrawCards();
        drawSystem.DrawCards();

        yield return new WaitForSeconds(1);
        Debug.Log("Players Turn");

        canPlayCards = true;
    }

    public void endPlayerTurn()
    {
        int i;
        for(i = 0; i < boardSystem.availableBoardSlots.Length; i++)
        {
            if(boardSystem.availableBoardSlots[i] == false)
            {
                boardSystem.boardSlots.
            }
        }

        canPlayCards = false;
        battleState = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        //AI turn
        orbSystem.resetOrbs();
        yield return new WaitForSeconds(1);
        Debug.Log("Enemy Turn");
        basicAI.playCard();
        turnCounter += 1;
        turnText.text = "TURN: " + turnCounter.ToString();
        turnText.enabled = true;
        yield return new WaitForSeconds(1.5f);
        endEnemyTurn();
        turnText.enabled = false;
    }

    public void endEnemyTurn()
    {
        battleState = BattleState.BATTLE;
        battleState = BattleState.PLAYERTURN;
        StartCoroutine(PlayersTurn());
    }
}
