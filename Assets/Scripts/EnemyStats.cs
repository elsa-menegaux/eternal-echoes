using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Unit
{

	[SerializeField] public string Name = "Placeholder Fella";
	[SerializeField] public int Level = 1;
	[SerializeField] public float damageModifier = 0.2F;
	[SerializeField] public int Reward = 10;
	[SerializeField] public float scaleModifier = 1;
	
	
	// Start is called before the first frame update
    private void Start()
    {
		InitStats();
    }
	
	//protected void DetectDeath(GameObject killer) 
	//{
	//	if (currentHealth <= 0)
	//	{
	//		OnDeath(killer);
	//	}
	//}
	
	private void OnDeath(GameObject killer)	
	{
		// Make sure the killer (player) has PlayerStats component
		PlayerStats playerStats = killer.GetComponent<PlayerStats>();
		if (playerStats != null)
		{
			playerStats.GainMoney(Reward); // Award money to the player
		}

		Destroy(gameObject); // Destroy enemy after death
	}
	
	public void ScaleStats(int rooms)	
	{
		if (rooms<=1)
		{
			Level = UnityEngine.Random.Range(1, 6);
		}
		else{
			Level = UnityEngine.Random.Range(1+(rooms*2), 6+(rooms*2));
		}
		float scalingFormula =(1+((scaleModifier * Level)/10));
		maxHealth = (int)(maxHealth * scalingFormula);
		currentHealth = maxHealth;
		currentDamage = (int)(currentDamage * scalingFormula);
		currentAbilityDamage = (int) (currentAbilityDamage * scalingFormula);
		currentCritChance = (currentCritChance * (scalingFormula/2));
		if (currentCritChance > 50f)
		{
			currentCritChance = 50f;
		}
		currentCritDamage = (currentCritDamage * (scalingFormula/2));
		currentDodgeRate = (currentDodgeRate * (scalingFormula/2));
		if (currentDodgeRate > 50f)
		{
			currentDodgeRate = 50f;
		}
	}
}
