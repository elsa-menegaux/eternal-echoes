using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int enemyStrength = 5;
	[SerializeField] private int enemyEndurance = 5;
	[SerializeField] private int enemyIntelligence = 5;
	[SerializeField] private int enemyReflexes = 5;
	[SerializeField] private int enemyTechnical = 5;
	[SerializeField] private int enemyReward = 10;
	
	private int currentHealth;
	private int currentDamage;
	private int currentAbilityDamage;
	private double currentCritChance;
	private double currentCritDamage;
	private double currentDodgeRate;
	
	
	private void Awake() {
	
	}
	
    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = enemyEndurance * 2; //10 default
		currentDamage = enemyStrength * 2; //10 default
		currentAbilityDamage = enemyIntelligence * 2; //10 default
		currentCritChance = enemyReflexes * 1.5 ; //7.5% default
		currentCritDamage = 2 * (enemyTechnical * 0.2); //2x mult default
		currentDodgeRate = enemyReflexes * 1.5; //7.5% default
    }
	
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
	}
	
	public void RegainHealth(int heal)
	{
		currentHealth += heal;
	}
	
	public void DetectDeath(GameObject killer) 
	{
		if (currentHealth <= 0)
		{
			OnDeath(killer);
		}
	}
	
	private void OnDeath(GameObject killer)	
	{
		// Make sure the killer (player) has PlayerStats component
		PlayerStats playerStats = killer.GetComponent<PlayerStats>();
		if (playerStats != null)
		{
			playerStats.GainMoney(enemyReward); // Award money to the player
		}

		Destroy(gameObject); // Destroy enemy after death
	}
}
