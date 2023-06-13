using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // --- This script is for BOTH player and dealer

    // Get other scripts
    public CardScript cardScript;   
    public DeckScript deckScript;

    // Total value of player/dealer's hand
    public int handValue = 0;

    // Array of card objects on table
    public GameObject[] hand;
    // Index of next card to be turned over
    public int cardIndex = 0;

    public int cardSelected = -1;
    public Text scoreText;


    public void StartHand()
    {
        GetCard();
        GetCard();
        cardSelected = -1;
    }

    // Add a hand to the player/dealer's hand
    private int GetCard()
    {
        // Get a card, use deal card to assign sprite and value to card on table
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // Show card on game screen
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // Add card value to running total of the hand
        handValue += cardValue;
        
        cardIndex++;
        UpdateText();
        return handValue;
    }

    public IEnumerator DrawCard()
    {
        // Get a card, use deal card to assign sprite and value to card on table
        deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // Show card on game screen
        hand[cardIndex].GetComponent<Renderer>().enabled = true;

        while (cardSelected == -1)
        {
            yield return null;
        }
        
        handValue -= hand[cardSelected].GetComponent<CardScript>().value;
        hand[cardSelected].GetComponent<CardScript>().SetSprite(hand[cardIndex].GetComponent<CardScript>().GetSprite());
        hand[cardSelected].GetComponent<CardScript>().SetValue(hand[cardIndex].GetComponent<CardScript>().value);
        hand[cardIndex].GetComponent<Renderer>().enabled = false;
        hand[cardSelected].GetComponent<Renderer>().enabled = true;
        handValue += hand[cardSelected].GetComponent<CardScript>().value;
        cardSelected = -1;
        UpdateText();
        Abs14Manager.instance.numOfTurns++;
        Abs14Manager.instance.swapingCards = false;
        yield return new WaitForSeconds(0.5f);
    }

    // Hides all cards, resets the needed variables
    public void ResetHand()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
    }

    public void UpdateText()
    {
        scoreText.text = "Hand: " + handValue + "\n"
            + "Score: " + Abs14Manager.instance.ClaculateResult(handValue);
    }
}
