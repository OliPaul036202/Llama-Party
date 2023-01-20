using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSystem : MonoBehaviour
{
    public Transform[] boardSlots;
    public bool[] availableBoardSlots;

    public Transform[] player2BoardSlots;
    public bool[] availablePlayer2Slots;
    public void playCard(GameObject card)
    {
        for (int i = 0; i < availableBoardSlots.Length; i++)
        {
            if (availableBoardSlots[i] == true)
            {
                Debug.Log("Spawned Board Card");
                Instantiate(card, boardSlots[i].position, card.transform.rotation);
                availableBoardSlots[i] = false;
                return;
            }
        }
    }

    public bool isPlayerSideAvailable()
    {
        for(int i = 0; i < availableBoardSlots.Length; i++)
        {
            if(availableBoardSlots[i] == true)
            {
                return true;
            }
        }
        return false;
    }

    public void player2PlayCard(GameObject card)
    {
        for (int i = 0; i < availablePlayer2Slots.Length; i++)
        {
            if (availablePlayer2Slots[i] == true)
            {
                Debug.Log("Spawned Board Card");
                Instantiate(card, player2BoardSlots[i].position, card.transform.rotation);
                availablePlayer2Slots[i] = false;
                return;
            }
        }
    }

    public bool isPlayer2SideAvailable()
    {
        for (int i = 0; i < availablePlayer2Slots.Length; i++)
        {
            if (availablePlayer2Slots[i] == true)
            {
                return true;
            }
        }
        return false;
    }
}
