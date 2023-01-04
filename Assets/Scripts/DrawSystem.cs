using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem : MonoBehaviour
{

    public List<GameObject> deck = new List<GameObject>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    
    public void DrawCards()
    {
        if(deck.Count >= 1)
        {
            GameObject randCard = deck[Random.Range(0, deck.Count)];

            for(int i = 0; i < availableCardSlots.Length; i++)
            {
                if(availableCardSlots[i] == true)
                {
                    randCard.SetActive(true);
                    randCard.transform.position = cardSlots[i].position;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }
}
