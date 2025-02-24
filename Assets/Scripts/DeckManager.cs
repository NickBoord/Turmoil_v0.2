using System.Collections;
using System.Collections.Generic;
using TurmoilStartup;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();

    public int startingHandSize = 3;

    private int currentIndex = 0;

    public int maxHandsize;

    public int currentHandSize;

    private HandManager handManager;

    private void Start()
    {
        //Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to the allCards list
        deck.AddRange(cards);

        handManager = FindObjectOfType<HandManager>();
        maxHandsize = 5;
        for (int i = 0; i < startingHandSize; i++)
        {
            Debug.Log($"Drawing Card");
            DrawCard(handManager);
        }
    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }


    public void DrawCard(HandManager handManager)
    {
        if (deck.Count == 0)
            return;
        if (currentHandSize < maxHandsize)
        {
            Card nextCard = deck[currentIndex];
            handManager.AddCardToHand(nextCard);
            currentIndex = (currentIndex + 1) % deck.Count;
        }
    }

}
