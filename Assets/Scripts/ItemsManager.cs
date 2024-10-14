using UnityEngine;

// This file handles item management, providing a centralized way to access and use items from the ItemDatabase.

public class ItemManager : MonoBehaviour
{
    public ItemDatabase itemDatabase; 

    public void UseItem(string itemName)
    {
        ItemData itemData = itemDatabase.GetItemDataByName(itemName); // Get item from Database
        if (itemData != null)
        {
            Debug.Log("Using item: " + itemData.itemName);

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
            }
            if (itemData.healthModifier != 0)
            {
                Debug.Log("Alter health: " + itemData.healthModifier);
            }
            if (itemData.damageModifier != 0)
            {
                Debug.Log("Alter damage: " + itemData.damageModifier);
            }
            if (itemData.abilityDamageModifier != 0)
            {
                Debug.Log("Alter abilityDamage: " + itemData.abilityDamageModifier);
            }
            if (itemData.critChanceModifier != 0)
            {
                Debug.Log("Alter critChance: " + itemData.critChanceModifier);
            }
            if (itemData.critDamageModifier != 0)
            {
                Debug.Log("Alter critDamage: " + itemData.critDamageModifier);
            }
            if (itemData.dodgeRateModifier != 0)
            {
                Debug.Log("Alter dodgeRate: " + itemData.dodgeRateModifier);
            }
        }
        else
        {
            Debug.LogError("Cannot find usable item: " + itemName);
        }
    }
}