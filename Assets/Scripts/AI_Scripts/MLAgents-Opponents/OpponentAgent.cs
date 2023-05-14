using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

/// <summary>
/// Opponent Machine Learning Agent
/// </summary>
public class OpponentAgent : Agent
{
    public bool canPlayTurn; // Check to see if its the opponents turn

    public List<CardHand> deck; // Get deck of opponent cards

    public OrbSystem orbSystem;
    public BoardSystem boardSystem;
    public TestingBattleSystem battleSystem;

    public ScoreSystem scoreSystem;
    public int currentScore;
    public int newScore;

    public bool IHaveCards;

    public bool trainingMode;

    private int action;

    /// <summary>
    /// Initialize the agent
    /// </summary>
    public override void Initialize()
    {
        orbSystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<OrbSystem>();
        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();
        battleSystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<TestingBattleSystem>();

        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();
        currentScore = scoreSystem.player2Score;
        newScore = 0;

        IHaveCards = false;

        if (!trainingMode) MaxStep = 0;
    }

    public override void OnEpisodeBegin()
    {
        IHaveCards = false;
        if (canPlayTurn)
        {
            RequestDecision();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(IHaveCards);
    }

    /// <summary>
    /// Agent plays a card from the selected action
    /// </summary>
    public void opponentChooseCard(ActionSegment<int> act)
    {
        //Choose and play three cards from three different branches
        playCard(act[0]);
        playCard(act[1]);
        playCard(act[2]);

        if(orbSystem.player2CurrentOrbs == 0)
        {
            battleSystem.endEnemyTurn();
        }
    }

    public void playCard(int deckNum)
    {
        Debug.Log("PLAYING CARD..." + deckNum.ToString());
        if (IHaveCards)
        {
            //Check to see if the selected card can be played. i.e has enough Llama Orbs to play
            if(deck.Count > 0)
            {
                if (deck[deckNum].orbCost <= orbSystem.player2CurrentOrbs && boardSystem.isPlayer2SideAvailable() && canPlayTurn)
                {
                    deck[deckNum].transform.position = transform.position; //Play card onto the board
                    deck.Remove(deck[deckNum]); //Remove card from the List
                    AddReward(0.1f); //Give reward for playing a card
                }
                else if (deck[deckNum].orbCost >= orbSystem.player2CurrentOrbs || !boardSystem.isPlayer2SideAvailable() || orbSystem.player2CurrentOrbs == 0)//If no more Llama Orbs or side of board is full, end turn
                {
                    battleSystem.endEnemyTurn();
                    canPlayTurn = false;
                    AddReward(0.05f); //Give reward for ending the turn
                }
            }
            else
            {
                canPlayTurn = false;
                battleSystem.endEnemyTurn();
            }
        }
    }

    /// <summary>
    /// Called when an action is requested
    /// 
    /// ActionBuffers represents the actions the Agent can take
    /// </summary>
    /// <param name="actions">The card to play</param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        IHaveCards = true;
        opponentChooseCard(actions.DiscreteActions);
    }
}
