using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOST}

    private BattleState battleState;
    private bool canPlayCards;

    public DrawSystem drawSystem;

    private OrbSystem orbSystem;
    // Start is called before the first frame update
    void Start()
    {
        orbSystem = GetComponent<OrbSystem>();
        canPlayCards = false;
        battleState = BattleState.START;
        StartCoroutine(BeginBattle());
    }

    IEnumerator BeginBattle()
    {
        Debug.Log("Starting Battle...");
        yield return new WaitForSeconds(2);
        //Wait 2 seconds then it is the players turn.
        battleState = BattleState.PLAYERTURN;

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
        yield return new WaitForSeconds(1);
        Debug.Log("Enemy Turn");

        //Wait 2 seconds then start players turn again **FOR TESTING PURPOSES**
        yield return new WaitForSeconds(2);
        battleState = BattleState.PLAYERTURN;
        yield return StartCoroutine(PlayersTurn());
    }

    
}
