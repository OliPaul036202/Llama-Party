using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI_Controller : MonoBehaviour
{
    public List<CardHandPlayer2> deck;
    public OrbSystem orbSystem;
    public BoardSystem boardSystem;

    private void Start()
    {
        orbSystem = GameObject.FindGameObjectWithTag("SystemsManager").GetComponent<OrbSystem>();

        boardSystem = GameObject.FindGameObjectWithTag("BoardSystem").GetComponent<BoardSystem>();
    }

    public void playCard()
    {
        int randCard = UnityEngine.Random.Range(0, deck.Count);

        if (deck[randCard].orbCost <= orbSystem.player2CurrentOrbs && boardSystem.isPlayer2SideAvailable())
        {
            //orbSystem.applyOrbCostPlayer2(deck[randCard].orbCost);
            deck[randCard].transform.position = transform.position;
            deck.RemoveAt(randCard);
        }
    }
}
