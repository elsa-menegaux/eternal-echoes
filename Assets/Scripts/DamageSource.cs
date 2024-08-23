using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
	[SerializeField] private int damageAmount = 1;
	
	private void OnTrigger(GameObject target)	
	{
		if (target.GetComponent<EnemyStats>())
		{
			EnemyStats enemyHealth = target.GetComponent<EnemyStats>();
			enemyHealth.TakeDamage(damageAmount);
		}
		if (target.GetComponent<PlayerStats>())
		{
			PlayerStats playerHealth = target.GetComponent<PlayerStats>();
			playerHealth.TakeDamage(damageAmount);
		}
	}
}
