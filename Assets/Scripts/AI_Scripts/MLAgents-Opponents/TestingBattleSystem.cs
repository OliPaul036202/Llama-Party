using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public GameObject endPanel;
    public TMP_Text winnerText;
    public TMP_Text turnText;

    //Get all player 1 current active cards
    [SerializeField]
    private BoardSystem boardSystem;

    //Demo AI
    public OpponentAgent opponentAgent; // **ML Agent** //
    public BasicAI_Controller basicAI; // **Custom Basic Bot** //

    bool trainingMode = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        endPanel.SetActive(false);

        orbSystem = GetComponent<OrbSystem>();
        drawSystem = GetComponent<DrawSystem>();

        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();


        //Get boardsystem to keep track active cards on board
        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();
        battleState = BattleState.START;
        StartCoroutine(BeginBattle());
    }

    public IEnumerator BeginBattle()
    {
        Debug.Log("Starting Battle...");
        turnCounter += 1;
        turnText.text = turnCounter.ToString();
        yield return new WaitForSeconds(0.01f); //Wait x seconds then it is Player 1 Agents turn

        //player1Agent.OnEpisodeBegin();

        opponentAgent.OnEpisodeBegin(); // Start training episode of agent
        
        opponentAgent.currentScore = scoreSystem.player2Score;

        battleState = BattleState.PLAYERONETURN;
        yield return StartCoroutine(PlayersTurn());
    }

    IEnumerator PlayersTurn()
    {
        orbSystem.resetOrbs();
        Debug.Log("Players 1s Turn");

        basicAI.playCard();

        yield return new WaitForSeconds(0.5f);
        basicAI.playCard();

        yield return new WaitForSeconds(0.5f);

        basicAI.playCard();

        yield return new WaitForSeconds(0.5f);

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
        opponentAgent.canPlayTurn = true;

        opponentAgent.RequestDecision();

        opponentAgent.newScore = scoreSystem.player2Score;
        if (opponentAgent.newScore < opponentAgent.currentScore)
        {
            //Take away reward if the Agents score has gone down since last turn
            opponentAgent.AddReward(-0.1f);
            opponentAgent.currentScore = scoreSystem.player2Score;
        }
        else if (opponentAgent.newScore > opponentAgent.currentScore)
        {
            //Give reward if the agents score has gone up since last turn
            opponentAgent.AddReward(0.5f);
            opponentAgent.currentScore = scoreSystem.player2Score;
        }

        //Add rewards if Agent has more Llama Points above 
        if (scoreSystem.player2Score > scoreSystem.playerScore)
        {
            opponentAgent.AddReward(0.5f);
        }

        yield return new WaitForSeconds(1.5f);

        opponentAgent.RequestDecision();

        turnCounter += 1;
        turnText.text = turnCounter.ToString();

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
            endPanel.SetActive(true);
            winnerText.text = "BASIC BOT WINS";
        }else if (scoreSystem.playerScore < scoreSystem.player2Score)
        {
            Debug.Log("PLAYER 2 WINS!");
            endPanel.SetActive(true);
            winnerText.text = "ML AGENT WINS";
        }else if(scoreSystem.playerScore == scoreSystem.player2Score)
        {
            endPanel.SetActive(true);
            winnerText.text = "DRAW";
        }

        if (trainingMode)
        {
            turnCounter = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }else if (!trainingMode)
        {
            Time.timeScale = 0;
            turnCounter = 0;
        }
        
    }
}
