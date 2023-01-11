using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public int orbCost;
    public int llamaPoints;

    public bool Booster;
    public bool Defender;
    public bool Attacker;
}
