using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dictionary<string, bool> enemyStatus = new Dictionary<string, bool>();

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
}
