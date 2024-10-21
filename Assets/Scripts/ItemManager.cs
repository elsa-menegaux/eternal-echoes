using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Collections;

// This file handles item management, providing a centralized way to access and use items from the ItemDatabase.

public class ItemManager : MonoBehaviour
{
    // Making the itemManager a singleton
    public static ItemManager Instance {get; private set; }
    public ItemsDatabase itemDatabase; 
    private PlayerStats playerStats;
    public PlayerManager playerManager;
    public Text dialogueText;
    public GameObject dialoguePanel;

    private void Awake()
    {
        // Ensure there's only one instance of ItemManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("ItemManager instantiated.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
            Debug.Log("Duplicate ItemManager destroyed.");
        }
    }
    private void Start()
    {
        // Ensure correct initialization
        dialoguePanel.gameObject.SetActive(false);
       playerManager = FindObjectOfType<PlayerManager>();
       if (playerManager != null)
       {
        playerStats = playerManager.playerStats;
        Debug.Log("Found PlayerManager");
       }
       else
       {
        Debug.LogError("PlayerManager not found.");
       }
    }

    public void UseItem(string itemName)
    {
        ItemsDatabase.ItemData itemData = itemDatabase.GetItemDataByName(itemName); // Get item from Database
        if (itemData != null)
        {
            // Using an item applies its modifiers and displays a text box describing what happened.
            Debug.Log("Using item: " + itemData.itemName);
            string feedbackPhrase = ApplyModifier(itemData);

            dialoguePanel.gameObject.SetActive(true);
            dialogueText.text = "You used the " + itemData.itemName + "! " + feedbackPhrase;
        
            StartCoroutine(HidePanelAfterDelay(5f));
        }
        else
        {
            Debug.LogError("Cannot find usable item: " + itemName);
        }
    }

    public string ApplyModifier(ItemsDatabase.ItemData itemData)
    {
        // Each modification of stats will add to the dialogue text feedback
        StringBuilder feedbackPhrase = new StringBuilder();

        // Check which stats the item alters and change it 
            if (itemData.maxHealthModifier != 0)
            {
                Debug.Log("Alter maxHealth: " + itemData.maxHealthModifier);
                playerStats.maxHealth += itemData.maxHealthModifier;
                // Heal the player at the same time because their maxHealth got maxed
                playerStats.currentHealth = itemData.maxHealthModifier;
                feedbackPhrase.Append("Max health increased by " + itemData.maxHealthModifier + ". ");
            }
            if (itemData.healthModifier != 0)
            {
                Debug.Log("Alter health: " + itemData.healthModifier);
                playerStats.currentHealth += itemData.healthModifier;
                // Ensure current health does not exceed max health
                playerStats.currentHealth = Mathf.Min(playerStats.currentHealth, playerStats.maxHealth);
                feedbackPhrase.Append("Health restored by " + itemData.healthModifier + ". ");
            }
            if (itemData.damageModifier != 0)
            {
                Debug.Log("Alter damage: " + itemData.damageModifier);
                playerStats.currentDamage += itemData.damageModifier;
                feedbackPhrase.Append("Damage increased by " + itemData.damageModifier + ". ");
            }
            if (itemData.abilityDamageModifier != 0)
            {
                Debug.Log("Alter abilityDamage: " + itemData.abilityDamageModifier);
                playerStats.currentAbilityDamage += itemData.abilityDamageModifier;
                feedbackPhrase.Append("Ability damage increased by " + itemData.abilityDamageModifier + ". ");
            }
            if (itemData.critChanceModifier != 0)
            {
                Debug.Log("Alter critChance: " + itemData.critChanceModifier);
                playerStats.currentCritChance += itemData.critChanceModifier;
                feedbackPhrase.Append("Crit chance increased by " + itemData.critChanceModifier + ". ");
            }
            if (itemData.critDamageModifier != 0)
            {
                Debug.Log("Alter critDamage: " + itemData.critDamageModifier);
                playerStats.currentCritDamage += itemData.critDamageModifier;
                feedbackPhrase.Append("Crit damage increased by " + itemData.critDamageModifier + ". ");
            }
            if (itemData.dodgeRateModifier != 0)
            {
                Debug.Log("Alter dodgeRate: " + itemData.dodgeRateModifier);
                playerStats.currentDodgeRate += itemData.dodgeRateModifier;
                feedbackPhrase.Append("Dodge rate increased by " + itemData.dodgeRateModifier + ". ");
            }
            return feedbackPhrase.ToString();
    }

    private IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        dialoguePanel.SetActive(false);
    }

    // For testing purposes
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.U))
    //     {
    //         Debug.Log("Keydown");
    //         if (ItemManager.Instance != null)
    //         {
    //             ItemManager.Instance.UseItem("Bubble Shield");
    //         }
    //         else
    //         {
    //             Debug.LogError("ItemManager.Instance is null.");
    //         }
    //     }
    // }
}
