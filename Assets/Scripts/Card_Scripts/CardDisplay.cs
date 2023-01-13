using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text nameText;
    public Text typeText;
    public Text descriptionText;

    public Image artworkImage;

    public Text costText;
    public Text valueText;

    void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;

        artworkImage.sprite = card.artwork;

        costText.text = card.orbCost.ToString();
        valueText.text = card.llamaPoints.ToString();

        if (card.Booster)
        {
            typeText.text = "BOOSTER";
        }else if (card.Defender)
        {
            typeText.text = "DEFENDER";
        }else if (card.Attacker)
        {
            typeText.text = "ATTACKER";
        }
    }

    public void RefreshDisplay()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;

        artworkImage.sprite = card.artwork;

        costText.text = card.orbCost.ToString();
        valueText.text = card.llamaPoints.ToString();

        if (card.Booster)
        {
            typeText.text = "BOOSTER";
        }
        else if (card.Defender)
        {
            typeText.text = "DEFENDER";
        }
        else if (card.Attacker)
        {
            typeText.text = "ATTACKER";
        }
    }
}
