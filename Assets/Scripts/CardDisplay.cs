using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TurmoilStartup;
using System;
using System.Collections.Generic;

public class CardDisplay : MonoBehaviour
{   public Card cardData;   
    public Image cardImage;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image[] typeImages;

    void Start()
    {
        UpdateCardDisplay();
    } 

   public void UpdateCardDisplay()
    {  
        nameText.text = cardData.cardname;
        healthText.text = cardData.health.ToString();
        damageText.text = cardData.damage.ToString();   
    }

}