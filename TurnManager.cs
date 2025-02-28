using System.Collections;
using System.Collections.Generic;
using TMPro;
using TurmoilStartup;
using UnityEngine;
using UnityEngine.UI;  // For Button and Slider support

public class TurnManager : MonoBehaviour
{
    public int cardsPlayed;
    public int cardsDiscarded;
    public int cardsDrawn;
    public int cardsInDeck;
    public int cardsInDiscard;
    public int cardsInHand;

    public TMP_Text deckButton;
    public TMP_Text discardButton;
   
    public Slider playerHealthBar;  // Text UI for player health

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
        cardsInHand = 3;

        enemyDreamBar = GetComponent<EnemyDreamBar>();
        playerHealth = GetComponent<PlayerHealth>();

        deckManager = FindObjectOfType<DeckManager>();
        handManager = FindObjectOfType<HandManager>();

        if(endTurnButton != null)
        {
            endTurnButton.onClick.AddListener(PlayerTurnEnd);
        }

    }

    void Update()
    {
        
    }

    public void PlayerTurnStart()
    {
        deckManager.DrawCard(handManager);
    }

    public void PlayCard(Card selectedCard)
    {

        ApplyCardEffects(selectedCard);
    }

    private void ApplyCardEffects(Card card)
    {
        foreach (var damageType in card.damageType)
        {
            switch (damageType)
            {
                case DamageType.anger:
                    enemyDreamBar?.ApplyDreamValue(card.damage);
                    break;
                case DamageType.Pacify:
                case DamageType.Healing:
                    enemyDreamBar?.ApplyDreamValue(-card.damage);
                    break;
                default:
                    Debug.LogWarning($"Unhandled damage type: {damageType}");
                    break;
            }
        }
    }

 

    public void PlayerTurnEnd()
    {
        cardsPlayed++;
        deckManager.DrawCard(handManager);
        EnemyTurnStart();
    }

    public void EnemyTurnStart()
    {
        if (enemyDreamBar != null && enemyDreamBar.ShouldAttack())
        {
            Debug.Log("Enemy attacks!");
            int damageToPlayer = Mathf.Max(0, (int)enemyDreamBar.currentValue);
            playerHealth?.TakeDamage(damageToPlayer);
        }
        else
        {
            Debug.Log("Enemy pacifies or does nothing.");
        }
    }
}
