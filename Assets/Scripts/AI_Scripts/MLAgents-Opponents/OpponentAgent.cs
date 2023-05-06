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
        RequestDecision();
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
            if (deck[deckNum].orbCost <= orbSystem.player2CurrentOrbs && boardSystem.isPlayer2SideAvailable())
            {
                deck[deckNum].transform.position = transform.position;
                deck.Remove(deck[deckNum]);
                AddReward(0.1f); //Give reward for playing a card
            }

            if(deck[deckNum].orbCost >= orbSystem.player2CurrentOrbs || !boardSystem.isPlayer2SideAvailable())
            {
                battleSystem.endEnemyTurn();
                AddReward(0.05f); //Give reward for ending the turn
            }
        }
    }

    /// <summary>
    /// Called every step of the engine. Here the agent will pick and play a card
    /// 
    /// ActionBuffers represents
    /// Index 0: First card in deck
    /// </summary>
    /// <param name="actions">The card to play</param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        //if (!canPlayTurn) return;
        IHaveCards = true;
        opponentChooseCard(actions.DiscreteActions);
    }
}
