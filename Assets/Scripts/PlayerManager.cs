using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
		OverworldHUD.SetHUD(playerStats);
    }
	
	void update()
	{
		OverworldHUD.SetHP(playerStats.currentHealth);
	}
	
	private void Start()
	{
		// Assign PlayerStats from the existing player GameObject
		playerStats = GetComponentInChildren<PlayerStats>();
	}
}
