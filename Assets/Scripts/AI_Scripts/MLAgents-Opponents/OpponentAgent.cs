using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

/// <summary>
/// Opponent Machine Learning Agent
/// </summary>
public class OpponentAgent : Agent
{
    public bool canPlayTurn; // Check to see if its the opponents turn

    public List<CardHand> deck; // Get deck of opponent cards

    public OrbSystem orbSystem;
    public BoardSystem boardSystem;
    public BattleSystem battleSystem;

    public ScoreSystem scoreSystem;
    private int currentScore;
    private int newScore;

    public bool trainingMode;

    /// <summary>
    /// Initialize the agent
    /// </summary>
    public override void Initialize()
    {
        orbSystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<OrbSystem>();
        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();
        battleSystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<BattleSystem>();

        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();
        currentScore = scoreSystem.player2Score;
        newScore = 0;

        if (!trainingMode) MaxStep = 0;
    }

    public override void OnEpisodeBegin()
    {
        canPlayTurn = false;
    }

    /// <summary>
    /// Agent plays a card
    /// </summary>
    public void opponentChooseCard(ActionSegment<int> act)
    {
        var action = act[0];
        //Debug.Log("Opponent PLAYING CARD..." + act.ToString());
        switch (action)
        {
            case 1:
                playCard(0);
                break;
            case 2:
                playCard(1);
                break;
            case 3:
                playCard(2);
                break;
            case 4:
                playCard(3);
                break;
            case 5:
                playCard(4);
                break;
        }

        //playCard(action);
    }

    public void playCard(int deckNum)
    {
        Debug.Log("PLAYING CARD..." + deckNum.ToString());
        deck[deckNum].transform.position = transform.position;
        AddReward(0.1f); //Give reward for playing a card
        battleSystem.endEnemyTurn();

        /*        if(deck[deckNum].orbCost <= orbSystem.player2CurrentOrbs && boardSystem.isPlayer2SideAvailable())
                {

                    newScore = scoreSystem.player2Score;
                }
                else
                {
                    // End opponent turn
                    canPlayTurn = false;
                    if(newScore >= currentScore)
                    {
                        AddReward(.03f); // Give reward if the Agents score has gone up or stayed the same since last round.
                        currentScore = newScore;
                    }
                    battleSystem.endEnemyTurn();
                }*/
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

        Debug.Log("RECEIVING ACTION...");
        opponentChooseCard(actions.DiscreteActions);
    }
}
