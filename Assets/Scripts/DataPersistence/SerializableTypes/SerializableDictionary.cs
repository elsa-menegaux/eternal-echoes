using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    // Constructor that accepts a Dictionary
    public SerializableDictionary(Dictionary<TKey, TValue> dictionary) : base(dictionary)
    {
        foreach (var pair in dictionary)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public SerializableDictionary() {}
    
    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    //load the dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary, but the number of keys ("+keys.Count+") does not match the number of values ("+values.Count+")");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }
}
