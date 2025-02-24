using System.Collections.Generic;
using UnityEngine;

 namespace TurmoilStartup{
    [CreateAssetMenu(fileName ="New Card",menuName ="Card")]
    public class Card : ScriptableObject{

        public string cardname;
        public List<CardType> cardType;
        public int damage;
        public int health;
        public Sprite cardSprite;

        public List<DamageType> damageType;
    }

    public enum  CardType{
        anger,
        Pacify
    }

    public enum DamageType{
        anger,
        Pacify,
        Healing
    }
 }

 