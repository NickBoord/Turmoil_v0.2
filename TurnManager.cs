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
    public GameObject enemy;
    public TMP_Text playerHealthText;  // Text UI for player health

    private DeckManager deckManager;
    private HandManager handManager;
    private EnemyDreamBar enemyDreamBar;  // Reference to enemy dream bar
    private PlayerHealth playerHealth;    // Reference to player's health

    public Button endTurnButton;   // Reference to the button that ends the turn

    void Start()
    {
        InitializeGameState();
        GetComponentReferences();
        SetupEndTurnButton();
    }

    void Update()
    {
        UpdateUI();
    }

    private void InitializeGameState()
    {
        cardsDiscarded = 0;
        cardsDrawn = 0;
        cardsInDeck = 10;
        cardsInHand = 5;
    }

    private void GetComponentReferences()
    {
        if (enemy != null)
        {
            enemyDreamBar = enemy.GetComponent<EnemyDreamBar>();
        }
        else
        {
            Debug.LogError("Enemy GameObject is not assigned in the inspector.");
        }

        playerHealth = GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component is missing.");
        }

        deckManager = GetComponent<DeckManager>();
        handManager = GetComponent<HandManager>();

        if (deckManager == null)
        {
            Debug.LogError("DeckManager component is missing.");
        }

        if (handManager == null)
        {
            Debug.LogError("HandManager component is missing.");
        }
    }

    private void SetupEndTurnButton()
    {
        if (endTurnButton != null)
        {
            endTurnButton.onClick.AddListener(PlayerTurnEnd);
        }
        else
        {
            Debug.LogError("End Turn Button is not assigned in the inspector.");
        }
    }

    private void UpdateUI()
    {
        if (playerHealth != null && playerHealthText != null)
        {
            playerHealthText.text = $"Player Health: {playerHealth.healthBar.value}";
        }
    }

    public void PlayerTurnStart()
    {
        if (deckManager == null || handManager == null)
        {
            Debug.LogError("DeckManager or HandManager is missing.");
            return;
        }
        deckManager.DrawCard(handManager);
    }

    public void PlayCard(Card selectedCard)
    {
        if (selectedCard == null)
        {
            Debug.Log("No card selected or card is null.");
            return;
        }

        ApplyCardEffects(selectedCard);
        DiscardCard(selectedCard);
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

    private void DiscardCard(Card discardedCard)
    {
        cardsDiscarded++;
        cardsInDiscard++;
        Debug.Log($"Card discarded: {discardedCard.cardname}");
    }

    public void PlayerTurnEnd()
    {
        cardsPlayed++;
        if (deckManager == null || handManager == null)
        {
            Debug.LogError("DeckManager or HandManager is missing.");
            return;
        }
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
