using UnityEngine;

// This file handles items Data storage and retrieval

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "ScriptableObjects/ItemsDatabase")]
public class ItemsDatabase : ScriptableObject
{
    [System.Serializable]
    public class ItemData
    {
        public string itemName;
        public Sprite sprite;
        public string description;

        // Since there is not large number of items and no complex behavior for them, all stats modifiers will be there below. 
        // Most of them relate to stats the player character has, or can have.
        public int strengthModifier;
        public int enduranceModifier;
        public int intelligenceModifier;
        public int reflexesModifier;
        public int technicalModifier;
        public int maxHealthModifier;
        public int healthModifier;
        public int damageModifier;
        public int abilityDamageModifier;
        public int critChanceModifier;
        public int critDamageModifier;
        public int dodgeRateModifier;
    }

    public ItemData[] itemDataArray;


    public ItemData GetItemDataByName(string itemName)
    {
        foreach (var itemData in itemDataArray)
        {
            if (itemData.itemName == itemName)
            {
                return itemData;
            }
        }
        // If not found
        Debug.LogWarning("Item not found in Database: " + itemName);
        return null; 
    }
}