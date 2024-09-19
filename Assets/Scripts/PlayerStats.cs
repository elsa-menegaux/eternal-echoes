using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Unit
{
	[SerializeField] public string Name = "Player";
	[SerializeField] public int Level = 1;
	[SerializeField] public float damageModifier = 0.2F;
	
	public int Money;
	
	private void Awake() 
	{
		// If you are instantiating a player object, ensure this script is on the PlayerManager
		if (PlayerManager.Instance != null && PlayerManager.Instance.playerStats != null)
		{
			// Set player stats from PlayerManager
			Name = PlayerManager.Instance.playerStats.Name;
			Level = PlayerManager.Instance.playerStats.Level;
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
    }
	
	public void GainMoney(int reward)
	{
		Money+=reward;
	}
}
