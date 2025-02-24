using System;
using System.Collections;
using System.Collections.Generic;
using TurmoilStartup;
using Unity.Mathematics;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefabUpdated;
    public Transform handTransform;
    public float fanSpread = 12.6f;
    public float cardSpacing = -14f;
    public float verticalSpacing = 100f;
    public int maxHandSize = 5;
    public List<GameObject> cardsInHand = new List<GameObject>();
   
    
    void Start()
    {
        
    }

   
    void Update() {
        //UpdateHandVisuals(); we might use this later, but not needed now. That's why I commented out rather than deleting.
    }

    public void AddCardToHand(Card cardData)
    {
        if (cardsInHand.Count < maxHandSize)
        {
            //Instantiate the card
            GameObject newCard = Instantiate(cardPrefabUpdated, handTransform.position, Quaternion.identity, handTransform);
            cardsInHand.Add(newCard);

            //set the data for the instantiated card
            newCard.GetComponent<CardDisplay>().cardData = cardData;
        }
            UpdateHandVisuals(); 
    }

    private void UpdateHandVisuals()
    {
         int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

         for(int i=0; i < cardCount; i++){
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);
           
            float horizontalOffSet = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);
            float verticalOffSet = verticalSpacing * (1 - normalizedPosition * normalizedPosition);

            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffSet, verticalOffSet, 0f);
         }
         
    }
}
