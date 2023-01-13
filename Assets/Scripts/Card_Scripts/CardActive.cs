using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardActive : MonoBehaviour
{
    public int llamaPoints;
    public int orbCost;

    public bool Defender;
    public bool Attacker;

    private ScoreSystem scoreSystem;

    public TMP_Text pointsText;

    public AbilityManager abilityManager;

    public GameObject previewCard;
    public Card activeCard;
    void Start()
    {
        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();
        abilityManager = GameObject.FindGameObjectWithTag("AbilityManager").GetComponent<AbilityManager>();

        previewCard = GameObject.FindGameObjectWithTag("PreviewCard");

        if (Defender)
        {
            scoreSystem.addPointsToPlayer(llamaPoints);
        }

        if (Attacker)
        {
            scoreSystem.takePointsFromOpponent(llamaPoints);
        }
    }

    private void Update()
    {
        pointsText.text = llamaPoints.ToString();
    }

    private void OnMouseOver()
    {
        previewCard.GetComponent<Animator>().SetBool("previewActive", true);
        previewCard.GetComponent<CardDisplay>().card = activeCard;
        previewCard.GetComponent<CardDisplay>().RefreshDisplay();

    }

    private void OnMouseExit()
    {
        previewCard.GetComponent<Animator>().SetBool("previewActive", false);
        previewCard.GetComponent<CardDisplay>().card = null;
    }
}
