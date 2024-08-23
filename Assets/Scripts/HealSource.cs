using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSource : MonoBehaviour
{
	[SerializeField] private int healAmount = 1;
	
	private void OnTrigger(GameObject target)
	{
		if (target.GetComponent<EnemyStats>())
		{
			EnemyStats enemyHealth = target.GetComponent<EnemyStats>();
			enemyHealth.RegainHealth(healAmount);
		}
		if (target.GetComponent<PlayerStats>())
		{
			PlayerStats playerHealth = target.GetComponent<PlayerStats>();
			playerHealth.RegainHealth(healAmount);
		}
	}   
}
