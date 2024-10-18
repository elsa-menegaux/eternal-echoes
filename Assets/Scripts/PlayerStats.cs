using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Unit, IDataPersistence
{
	[SerializeField] public string playerName = "Player";
	[SerializeField] public int level = 1;
	[SerializeField] public float damageModifier = 0.2F;
	
	public int money;
	
	private void Awake() 
	{
		// If you are instantiating a player object, ensure this script is on the PlayerManager
		if (PlayerManager.Instance != null && PlayerManager.Instance.playerStats != null)
		{
			// Set player stats from PlayerManager

			playerName = PlayerManager.Instance.playerStats.playerName;
			level = PlayerManager.Instance.playerStats.level;
			maxHealth = PlayerManager.Instance.playerStats.maxHealth;
			currentHealth = PlayerManager.Instance.playerStats.currentHealth;
			currentDamage = PlayerManager.Instance.playerStats.currentDamage; 
			currentAbilityDamage = PlayerManager.Instance.playerStats.currentAbilityDamage;
			currentCritChance = PlayerManager.Instance.playerStats.currentCritChance;
			currentCritDamage = PlayerManager.Instance.playerStats.currentCritDamage;
			currentDodgeRate = PlayerManager.Instance.playerStats.currentDodgeRate;
		}
		gameObject.SetActive(true);
	}
	
    // Start is called before the first frame update
    private void Start()
    {
		Debug.Log("Player Name: " + playerName);
        Debug.Log("Initial Health: " + currentHealth);
    }
	
	public void GainMoney(int reward)
	{
		money+=reward;
	}

	public void loadFromPlayerStatsData(PlayerStatsData playerStatsData)
	{
		this.playerName = playerStatsData.playerName;
        this.level = playerStatsData.level;
        this.damageModifier = playerStatsData.damageModifier;
        this.money = playerStatsData.money;

        this.maxHealth = playerStatsData.maxHealth;
        this.currentHealth = playerStatsData.currentHealth;
        this.currentDamage = playerStatsData.currentDamage;
        this.currentAbilityDamage = playerStatsData.currentAbilityDamage;
        this.currentCritChance = playerStatsData.currentCritChance;
        this.currentCritDamage = playerStatsData.currentCritDamage;
        this.currentDodgeRate = playerStatsData.currentDodgeRate;
        this.overrideStats = playerStatsData.overrideStats;
        this.overrideCalculatedHealth = playerStatsData.overrideCalculatedHealth;
	}

    public void LoadData(PersistentGameData persistentGameData)
    {
        this.loadFromPlayerStatsData(persistentGameData.playerStats);
    }

    public void SaveData(ref PersistentGameData persistentGameData)
    {
        persistentGameData.playerStats = new PlayerStatsData(this);
    }
}
