using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSystem : MonoBehaviour
{
    public Transform[] boardSlots;
    public bool[] availableBoardSlots;

    public void playCard(GameObject card)
    {
        for (int i = 0; i < availableBoardSlots.Length; i++)
        {
            if (availableBoardSlots[i] == true)
            {
                Debug.Log("Spawned Board Card");
                Instantiate(card, boardSlots[i].position, Quaternion.identity);
                availableBoardSlots[i] = false;
                return;
            }
        }
    }
}
