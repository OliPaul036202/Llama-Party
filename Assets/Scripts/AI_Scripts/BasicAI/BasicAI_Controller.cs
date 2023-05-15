using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI_Controller : MonoBehaviour
{
    public List<CardHand> deck;
    public OrbSystem orbSystem;
    public BoardSystem boardSystem;
    public TestingBattleSystem battleStystem;

    public bool isAgainstAI = false;

    private void Start()
    {
        orbSystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<OrbSystem>();

        if (isAgainstAI)
        {
            battleStystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<TestingBattleSystem>();
        }

        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();
    }

    public void playCard()
    {
        int randCard = UnityEngine.Random.Range(0, deck.Count);

        if (deck[randCard].orbCost <= orbSystem.playerCurrentOrbs && boardSystem.isPlayerSideAvailable())
        {
            Debug.Log("Playing Card" + randCard.ToString());
            deck[randCard].transform.position = transform.position;
            deck.RemoveAt(randCard);
        }
        else if(deck[randCard].orbCost > orbSystem.playerCurrentOrbs || !boardSystem.isPlayerSideAvailable() || deck.Count <= 0)
        {
            if (isAgainstAI)
            {
                battleStystem.endPlayerTurn();
            }
        }
    }

    public void playCardEnemy()
    {
        int randCard = UnityEngine.Random.Range(0, deck.Count);

        if (deck[randCard].orbCost <= orbSystem.player2CurrentOrbs && boardSystem.isPlayer2SideAvailable())
        {
            Debug.Log("Playing Card" + randCard.ToString());
            deck[randCard].transform.position = transform.position;
            deck.RemoveAt(randCard);
        }
    }
}
