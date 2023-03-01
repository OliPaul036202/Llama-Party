using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI_Controller : MonoBehaviour
{
    public List<CardHandPlayer2> deck;

    public void playCard()
    {
        int randCard = UnityEngine.Random.Range(0, deck.Count);

        deck[randCard].transform.position = transform.position;
    }
}
