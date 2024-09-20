using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	[SerializeField] protected int startingStrength = 5;
	[SerializeField] protected int startingEndurance = 5;
	[SerializeField] protected int startingIntelligence = 5;
	[SerializeField] protected int startingReflexes = 5;
	[SerializeField] protected int startingTechnical = 5;
	
	public int maxHealth;
	public float currentHealth;
	public int currentDamage;
	public int currentAbilityDamage;
	public float currentCritChance;
	public float currentCritDamage;
	public float currentDodgeRate;
	public bool overrideStats;
	public bool overrideCalculatedHealth;
	
	protected void GetStats()
	{
		if (overrideStats)
		{		
		}
		else
		{
			maxHealth = startingEndurance * 2; //10 default
			currentDamage = startingStrength * 2; //10 default
			currentAbilityDamage = startingIntelligence * 2; //10 default
			currentCritChance = startingReflexes * 1.5F ; //7.5% default
			currentCritDamage = 2 * (startingTechnical * 0.2F); //2x mult default
			currentDodgeRate = startingReflexes * 1.5F; //7.5% default
			if (overrideCalculatedHealth)
			{
			}
			else
			{
				currentHealth = maxHealth;
			}
		}
		
	}

	
	public bool TakeDamage(float damage)
	{
		if ((currentHealth - damage) <=0)
		{
			currentHealth=0;
		}
		else
		{
			currentHealth -= damage;
		}
		bool deathState = DetectDeath();
		return deathState;
	}
	
	public void RegainHealth(float heal)
	{
		if ((currentHealth + heal) >=maxHealth)
		{
			currentHealth = maxHealth;
		}
		else
		{
			currentHealth += heal;
		}
	}
	
	public bool DetectDeath() 
	{
		if (currentHealth <= 0)
		{
			return true;
		}
		else
			return false;
	}
}