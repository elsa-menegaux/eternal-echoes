using UnityEngine;

[CreateAssetMenu(fileName = "ItemsDatabaseTest", menuName = "ScriptableObjects/ItemsDatabaseTest")]
public class ItemsDatabaseTest : ScriptableObject
{
    [System.Serializable]
    public class ItemData
    {
        public string itemName;
        public Sprite sprite;
        public string description;

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