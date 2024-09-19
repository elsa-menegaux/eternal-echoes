using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public PlayerStats playerStats;
    public PlayerBattleHUD OverworldHUD;

    private void Awake()
    {
        // Ensure there's only one instance of PlayerManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerManager instantiated.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Start()
    {
        // Assign PlayerStats from the existing player GameObject
        playerStats = GetComponentInChildren<PlayerStats>();

        // Automatically find and assign PlayerBattleHUD
        OverworldHUD = FindObjectOfType<PlayerBattleHUD>();
        if (OverworldHUD != null)
        {
            Debug.Log("OverworldHUD found and assigned.");
            OverworldHUD.SetHUD(playerStats);
        }
        else
        {
            Debug.LogError("OverworldHUD not found!");
        }

        if (playerStats != null)
        {
            Debug.Log("PlayerStats assigned: " + playerStats.Name);
        }
        else
        {
            Debug.LogError("PlayerStats not found!");
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
		if (scene.name == "OverworldTestScene") // replace with actual scene name
		{
			OverworldHUD = FindObjectOfType<PlayerBattleHUD>();
			if (OverworldHUD != null)
			{
				OverworldHUD.SetHUD(playerStats);
			}
			else
			{
				Debug.LogError("OverworldHUD not found after scene load!");
			}
		}
	}

    private void Update()
    {
        if (playerStats != null && OverworldHUD != null)
        {
            OverworldHUD.SetHP(playerStats.currentHealth);
        }
    }
}
