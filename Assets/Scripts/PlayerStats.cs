using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	[SerializeField] private int startingPlayerStrength = 5;
	[SerializeField] private int startingPlayerEndurance = 5;
	[SerializeField] private int startingPlayerIntelligence = 5;
	[SerializeField] private int startingPlayerReflexes = 5;
	[SerializeField] private int startingPlayerTechnical = 5;
	
	private int currentHealth;
	private int currentDamage;
	private int currentAbilityDamage;
	private double currentCritChance;
	private double currentCritDamage;
	private double currentDodgeRate;
	private int money;
	private bool gameOver;
	
	
	private void Awake() {
	
	}
	
    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = startingPlayerEndurance * 2; //10 default
		currentDamage = startingPlayerStrength * 2; //10 default
		currentAbilityDamage = startingPlayerIntelligence * 2; //10 default
		currentCritChance = startingPlayerReflexes * 1.5 ; //7.5% default
		currentCritDamage = 2 * (startingPlayerTechnical * 0.2); //2x mult default
		currentDodgeRate = startingPlayerReflexes * 1.5; //7.5% default
		money = 0;
    }
	
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
	}
	
	public void RegainHealth(int heal)
	{
		currentHealth += heal;
	}
	
	public void DetectDeath() 
	{
		if (currentHealth <= 0)
		{
			gameOver=true;
			Destroy(gameObject);
		}
	}
	
	public void GainMoney(int reward)
	{
		money+=reward;
	}
}
