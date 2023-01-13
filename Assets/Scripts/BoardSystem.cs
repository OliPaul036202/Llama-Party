using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSystem : MonoBehaviour
{
    public Transform[] boardSlots;
    public bool[] availableBoardSlots;

    public int slotCounter;
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
}
