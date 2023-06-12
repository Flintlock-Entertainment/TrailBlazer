using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Abs14Manager : MonoBehaviour
{
    static public Abs14Manager instance;
    // Game Buttons
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;

    private int standClicks = 0;

    // Access the player and dealer's script
    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    // public Text to access and update - hud
    public Text scoreTextPlayer;
    public Text ScoreTextDealer;
    public Text mainText;
    public Text standBtnText;

    // hiding dealer's cards
    public GameObject hideCards;
    
    public int numOfTurns = 1;

    private bool dealerFinished;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // Add on click listeners to the buttons
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        //betBtn.onClick.AddListener(() => BetClicked());
    }

    private void DealClicked()
    {
        // Reset round, hide text, prep for new hand
        playerScript.ResetHand();
        dealerScript.ResetHand();

        new WaitForSeconds(0.1f);
        // Hide deal hand score at start of deal
        ScoreTextDealer.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        ScoreTextDealer.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        // Place card back on dealer card, hide card
        //hideCards.GetComponent<Renderer>().enabled = true;
        hideCards.SetActive(true);
        // Adjust buttons visibility
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";
        standClicks = 0;
        numOfTurns = 1;
        dealerFinished = false;
    }

    private void HitClicked()
    {
        Debug.Log("Hit");
        playerScript.cardSelected = -1;
        if (numOfTurns <= 3 && standClicks == 0)
        {
            StartCoroutine(playerScript.DrawCard());
        }
    }

    public void UpdateTurn()
    {
        numOfTurns++;
    }
    private void StandClicked()
    {
        standClicks++;
        if (standClicks > 1 &&  dealerFinished) 
            RoundOver();
        if(standClicks == 1)
            StartCoroutine( HitDealer());
        standBtnText.text = "Wait";
    }

    private IEnumerator HitDealer()
    {
        numOfTurns = 1;
        Debug.Log("Dealers turn");
        yield return new WaitForSeconds(0.2f);

        while (dealerScript.handValue < 20 && dealerScript.handValue > 8 && numOfTurns <= 3)
        {
            Debug.Log("Dealers turn loop");
            ChooseCard();
            yield return dealerScript.DrawCard();
        }
        dealerFinished = true;
        standBtnText.text = "Call";
        Debug.Log("Dealers finished");
    }

    // Check for winnner and loser, hand is over
    void RoundOver()
    {
        int playerValue = ClaculateResult(playerScript.handValue);
        int dealerValue = ClaculateResult(dealerScript.handValue);
        Debug.Log("player value: " + playerValue + " player hand: " + playerScript.handValue);
        Debug.Log("dealer value: " + dealerValue + " dealer hand: " + dealerScript.handValue);
        if (playerValue > dealerValue)
        {
            mainText.text = "You win!";
        }
        else if(dealerValue > playerValue)
        {
            mainText.text = "Dealer win!";
        }
        else
        {
            mainText.text = "Draw!";
        }

        // Set ui up for next move / hand / turn
        
        hitBtn.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(false);
        dealBtn.gameObject.SetActive(true);
        mainText.gameObject.SetActive(true);
        ScoreTextDealer.gameObject.SetActive(true);
        hideCards.SetActive(false);
    }
    public void SelectCard1()
    {
        playerScript.cardSelected = 0;
    }

    public void SelectCard2()
    {
        playerScript.cardSelected = 1;
    }

    public int ClaculateResult(int handValue)
    {
        return Mathf.Abs(14 - handValue);
    }

    private void ChooseCard()
    {
        dealerScript.cardSelected = 0;
        int dist0 = Mathf.Abs(dealerScript.hand[0].GetComponent<CardScript>().value - 7);
        int dist1 = Mathf.Abs(dealerScript.hand[1].GetComponent<CardScript>().value - 7);
        if (dist1 > dist0)
            dealerScript.cardSelected = 1;
    }
}
