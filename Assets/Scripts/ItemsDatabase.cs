using UnityEngine;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "ScriptableObjects/ItemsDatabase")]
public class ItemDatabase : ScriptableObject
{
    [System.Serializable]
    public class ItemData
    {
        public string itemName;
        public Sprite sprite;
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
        return null; // Return null if not found
    }
}