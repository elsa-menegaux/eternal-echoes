using System.Runtime.CompilerServices;
using UnityEngine;

// This file handles item management, providing a centralized way to access and use items from the ItemDatabase.

public class ItemManager : MonoBehaviour
{
    // Making the itemManager a singleton
    public static ItemManager Instance {get; private set; }
    public ItemsDatabase itemDatabase; 
    public PlayerStats playerStats;
    public PlayerManager playerManager;

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
            Debug.Log("Using item: " + itemData.itemName);
            ApplyModifier(itemData);
        }
        else
        {
            Debug.LogError("Cannot find usable item: " + itemName);
        }
    }

    public void ApplyModifier(ItemsDatabase.ItemData itemData)
    {
        // Check which stats the item alters and change it 
            if (itemData.strengthModifier != 0)
            {
                Debug.Log("Alter strength: " + itemData.strengthModifier);
                
            }
            if (itemData.enduranceModifier != 0)
            {
                Debug.Log("Alter endurance: " + itemData.enduranceModifier);
            }
            if (itemData.intelligenceModifier != 0)
            {
                Debug.Log("Alter intelligence: " + itemData.intelligenceModifier);
            }
            if (itemData.reflexesModifier != 0)
            {
                Debug.Log("Alter reflexes: " + itemData.reflexesModifier);
            }
            if (itemData.technicalModifier != 0)
            {
                Debug.Log("Alter technical: " + itemData.technicalModifier);
            }
            if (itemData.maxHealthModifier != 0)
            {
                Debug.Log("Alter maxHealth: " + itemData.maxHealthModifier);
                playerStats.maxHealth += itemData.maxHealthModifier;
                // Heal the player at the same time
                playerStats.currentHealth = itemData.maxHealthModifier;
            }
            if (itemData.healthModifier != 0)
            {
                Debug.Log("Alter health: " + itemData.healthModifier);
                playerStats.currentHealth += itemData.healthModifier;
                // Ensure current health does not exceed max health
                playerStats.currentHealth = Mathf.Min(playerStats.currentHealth, playerStats.maxHealth);
            }
            if (itemData.damageModifier != 0)
            {
                Debug.Log("Alter damage: " + itemData.damageModifier);
                playerStats.currentDamage += itemData.damageModifier;
            }
            if (itemData.abilityDamageModifier != 0)
            {
                Debug.Log("Alter abilityDamage: " + itemData.abilityDamageModifier);
                playerStats.currentAbilityDamage += itemData.abilityDamageModifier;
            }
            if (itemData.critChanceModifier != 0)
            {
                Debug.Log("Alter critChance: " + itemData.critChanceModifier);
                playerStats.currentCritChance += itemData.critChanceModifier;
            }
            if (itemData.critDamageModifier != 0)
            {
                Debug.Log("Alter critDamage: " + itemData.critDamageModifier);
                playerStats.currentCritDamage += itemData.critDamageModifier;
            }
            if (itemData.dodgeRateModifier != 0)
            {
                Debug.Log("Alter dodgeRate: " + itemData.dodgeRateModifier);
                playerStats.currentDodgeRate += itemData.dodgeRateModifier;
            }
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
