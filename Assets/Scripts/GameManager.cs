using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    //Below is a list of data that we will need to transfer between scenes: I'm adding all that I can think of, even if they aren't referenced yet.
    private int playerHealth;

    private int playerKarma; //working title atm; this will be what carries over the amount of enemies purified/killed
                             //I also think that the Karma system could be done and calculated in it's own script, then saved here. 

    private int angerCardsPlayed; //These two are here in case the Karma system script doesn't work on it's own. We can track total pure/anger cards here.
    private int purifyCardsPlayed;

    private int gainedCards;

    private int gainedMemories;

    public OptionsManager OptionsManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public DeckManager DeckManager { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    

    private void InitializeManagers()
    {
        OptionsManager = GetComponentInChildren<OptionsManager>();
        AudioManager = GetComponentInChildren<AudioManager>();
        DeckManager = GetComponentInChildren<DeckManager>();

        if (OptionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if (prefab == null)
            {
                Debug.Log($"OptionsManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                OptionsManager = GetComponentInChildren<OptionsManager>();
            }
        }
        if (AudioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
            {
                Debug.Log($"AudioManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                AudioManager = GetComponentInChildren<AudioManager>();
            }
        }
        if (DeckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
            if (prefab == null)
            {
                Debug.Log($"DeckManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                DeckManager = GetComponentInChildren<DeckManager>();
            }
        }
    }

    public int PlayerHealth //note the capitalization here
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

}
