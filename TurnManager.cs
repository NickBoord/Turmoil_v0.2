using System.Collections;
using System.Collections.Generic;
using TMPro;
using TurmoilStartup;
using UnityEngine;
using UnityEngine.UI;  // For Button and Slider support

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
    public TMP_Text playerHealthText;  // Text UI for player health

    DeckManager deckManager;
    HandManager handManager;
    EnemyDreamBar enemyDreamBar;  // Reference to enemy dream bar
    PlayerHealth playerHealth;    // Reference to player's health

    public Button endTurnButton;   // Reference to the button that ends the turn

    void Start()
    {
        cardsDiscarded = 0;
        cardsDrawn = 0;
        cardsInDeck = 10;
        cardsInHand = 5;

        // Get references to the components
        enemyDreamBar = enemy.GetComponent<EnemyDreamBar>();
        playerHealth = GetComponent<PlayerHealth>();  // Get the PlayerHealth component

        if (endTurnButton != null)
        {
            endTurnButton.onClick.AddListener(PlayerTurnEnd);  // Add listener to end turn button
        }
        else
        {
            Debug.LogError("End Turn Button is not assigned in the inspector.");
        }
    }

    void Update()
    {
        // Update UI or other logic if necessary
    }

    public void PlayerTurnStart()
    {
        deckManager.DrawCard(handManager);  // Drawing a card at the start of the player's turn
    }

    public void PlayCard(Card selectedCard)
    {
        // Handle card based on its type
        if (selectedCard != null)
        {
            // Apply card effect to enemy's dream bar
            foreach (var damageType in selectedCard.damageType)
            {
                if (damageType == DamageType.anger)
                {
                    enemyDreamBar.ApplyDreamValue(selectedCard.damage);  // Increase anger in the dream bar
                }
                else if (damageType == DamageType.Pacify)
                {
                    enemyDreamBar.ApplyDreamValue(-selectedCard.damage);  // Reduce anger in the dream bar
                }
                else if (damageType == DamageType.Healing)
                {
                    // Healing (if you need to apply healing logic)
                    enemyDreamBar.ApplyDreamValue(-selectedCard.damage);  // Healing can pacify the enemy
                }
            }

             
            DiscardCard(selectedCard);
        }
        else
        {
            Debug.Log("No card selected or card is null.");
        }
    }

     
    private void DiscardCard(Card discardedCard)
    {
        cardsDiscarded++;
        Debug.Log("Card discarded: " + discardedCard.cardname);

        
        cardsInDiscard++;
    }
     
    public void PlayerTurnEnd()
    {
        cardsPlayed++;  

       
        deckManager.DrawCard(handManager);

        
        EnemyTurnStart();
    }

 
    public void EnemyTurnStart()
    {
     
        if (enemyDreamBar.ShouldAttack())
        {
            Debug.Log("Enemy attacks!");
            int damageToPlayer = Mathf.Max(0, (int)enemyDreamBar.currentValue);
            playerHealth.TakeDamage(damageToPlayer);   

 
            playerHealthText.text = "Player Health: " + playerHealth.healthBar.value;
        }
        else
        {
            Debug.Log("Enemy pacifies or does nothing.");
        }

        
    }
}