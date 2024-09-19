using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Unit
{

	[SerializeField] public string Name = "Placeholder Fella";
	[SerializeField] public int Level = 1;
	[SerializeField] public float damageModifier = 0.2F;
	[SerializeField] public int Reward = 10;
	
	
	// Start is called before the first frame update
    private void Start()
    {
		if (GameDataHolder.enemyRespawnTest==false)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
		}
		GetStats();
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
}
