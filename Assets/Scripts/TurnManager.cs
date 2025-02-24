using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    private int cardsPlayed;
    private int cardsDiscarded;
    private int cardsDrawn;
    private int cardsInDeck;
    private int cardsInDiscard;
    private int cardsInHand;

    public TMP_Text deckButton;
    public TMP_Text discardButton;
    public GameObject enemy;

    DeckManager deckManager;
    HandManager handManager;
    EnemyTarget enemyTarget;

    void Start()
    {
        cardsDiscarded = 0;
        cardsDrawn = 0;
        cardsInDeck = 10;
    }

    
    void Update()
    {
        
    }

    public void PlayerTurnStart()
    {
        deckManager.DrawCard(handManager);
    }

    public void PlayCard()
    {
       
    }

    public void PlayerTurnEnd()
    {

    }

    public void EnemyTurnStart()
    {

    }
}
