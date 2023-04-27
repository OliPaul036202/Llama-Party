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

    public bool player1BoardCard;
    public bool player2BoardCard;

    private ScoreSystem scoreSystem;

    public TMP_Text pointsText;

    public AbilityManager abilityManager;

    public GameObject previewCard;

    public Card activeCard;

    private AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();
        abilityManager = GameObject.FindGameObjectWithTag("AbilityManager").GetComponent<AbilityManager>();

        previewCard = GameObject.FindGameObjectWithTag("PreviewCard");

        audioSource = this.GetComponent<AudioSource>();
        if (audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }

        if (player1BoardCard)
        {
            if (Defender)
            {
                scoreSystem.addPointsToPlayer(llamaPoints);
            }

            if (Attacker)
            {
                scoreSystem.takePointsFromPlayerTwo(llamaPoints);
            }
        }
        
        if (player2BoardCard)
        {
            if (Defender)
            {
                scoreSystem.addPointsToPlayerTwo(llamaPoints);
            }

            if (Attacker)
            {
                scoreSystem.takePointsFromPlayer(llamaPoints);
            }
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
        if (Defender)
        {
            previewCard.GetComponent<PreviewCard>().setDefenderBoarder();
        }else if (Attacker)
        {
            previewCard.GetComponent<PreviewCard>().setAttackerBoarder();
        }
        previewCard.GetComponent<CardDisplay>().RefreshDisplay();
    }

    private void OnMouseExit()
    {
        previewCard.GetComponent<Animator>().SetBool("previewActive", false);
        previewCard.GetComponent<CardDisplay>().card = null;
    }
}
