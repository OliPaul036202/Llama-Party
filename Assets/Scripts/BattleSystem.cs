using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOST}

    private BattleState battleState;
    private bool canPlayCards;

    public DrawSystem drawSystem;

    private OrbSystem orbSystem;

    private int turnCounter = 0;
    public Text turnText;

    public BasicAI_Controller basicAI; // **FOR TESTING** //
    // Start is called before the first frame update
    void Start()
    {
        turnText.enabled = false;
        orbSystem = GetComponent<OrbSystem>();
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
        turnText.enabled = false;
        endEnemyTurn();
    }

    public void endEnemyTurn()
    {
        battleState = BattleState.PLAYERTURN;
        StartCoroutine(PlayersTurn());
    }

    
}
