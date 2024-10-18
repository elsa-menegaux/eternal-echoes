using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager instance;
	
    public Dictionary<string, bool> enemyStatus = new Dictionary<string, bool>();

    public void LoadData(PersistentGameData persistentGameData)
    {
        enemyStatus = persistentGameData.enemyStatus;
    }

    public void SaveData(ref PersistentGameData persistentGameData)
    {
        persistentGameData.enemyStatus = new SerializableDictionary<string, bool>(enemyStatus);
    }

    private void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
			Debug.Log("GameManager instantiated.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }
	
	private void Start()
	{
		// Initialize enemyStatus if needed
		if (enemyStatus == null)
		{
			enemyStatus = new Dictionary<string, bool>();
			Debug.Log("GameManager instantiated.");
		}
		
	}

    private void Update()
    {
        if (DontDestroyOnLoadDestroyer.killAllObjects)
        {
            Destroy(gameObject);
        }
    }
}
