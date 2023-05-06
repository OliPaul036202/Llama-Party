using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    //public OpponentAgent player1Agent; // **FOR REAL** //

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

        //player1Agent.OnEpisodeBegin();

        opponentAgent.OnEpisodeBegin();
        opponentAgent.currentScore = scoreSystem.player2Score;

        battleState = BattleState.PLAYERONETURN;
        yield return StartCoroutine(PlayersTurn());
    }

    IEnumerator PlayersTurn()
    {
        orbSystem.resetOrbs();
        Debug.Log("Players 1s Turn");

        //player1Agent.RequestDecision();
        yield return new WaitForSeconds(0.1f);
        endPlayerTurn();
    }

    public void endPlayerTurn()
    {

        battleState = BattleState.PLAYERTWOTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Player 2s Turn");

        //Allow the Agent to play its turn.
        opponentAgent.RequestDecision();

        opponentAgent.newScore = scoreSystem.player2Score;
        if (opponentAgent.newScore < opponentAgent.currentScore)
        {
            opponentAgent.AddReward(-0.1f);
            opponentAgent.currentScore = scoreSystem.player2Score;
        }
        else if (opponentAgent.newScore > opponentAgent.currentScore)
        {
            opponentAgent.AddReward(0.5f);
            opponentAgent.currentScore = scoreSystem.player2Score;
        }

        if (scoreSystem.player2Score > scoreSystem.playerScore)
        {
            opponentAgent.AddReward(0.5f);
        }



        yield return new WaitForSeconds(0.01f);

        turnCounter += 1;
        audioSource.Play();

        //Check to see if the game has reached the end of turn 7
        if (turnCounter == 8)
        {
            //player1Agent.EndEpisode();
            opponentAgent.EndEpisode();
            endGame();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
