using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataHolder : MonoBehaviour
{
    public static GameDataHolder Instance;  // Singleton instance
    public EnemyStats enemyStats;
	public string previousSceneName;
	public string enemyname;

    void Awake()
    {
        // Ensure this object persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // This object won't be destroyed on scene load
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }
	
	private void Start()
	{
		// Initialize enemyStatus if needed
		if (enemyStats == null)
		{
			enemyStats = GetComponentInChildren<EnemyStats>();
			Debug.Log("EnemyStats instantiated.");
		}
	}
	
	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Check if the current scene is the overworld
		if (scene.name == "BattleScene") // replace with actual scene name
		{
			enemyStats = FindObjectOfType<EnemyStats>();
			if (enemyStats != null)
			{
				Debug.Log("enemyStats found after scene load!");
			}
			else
			{
				Debug.LogError("BattleScene not found after scene load!");
			}
		}
	}
}