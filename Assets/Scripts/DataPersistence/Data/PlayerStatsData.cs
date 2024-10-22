using System;

//Serializeable Data type for PlayerStats
[Serializable]
public class PlayerStatsData
{
    //Player Specific Variables
    public string playerName;
	public int level;
	public float damageModifier;
    public int money;

    //Unit Variables
    /*
    public int startingStrength;
	public int startingEndurance;
	public int startingIntelligence;
	public int startingReflexes;
	public int startingTechnical;
    */
	
	public int maxHealth;
	public float currentHealth;
	public int currentDamage;
	public int currentAbilityDamage;
	public float currentCritChance;
	public float currentCritDamage;
	public float currentDodgeRate;
	public bool overrideStats;
	public bool overrideCalculatedHealth;

    public PlayerStatsData(PlayerStats playerStats)
    {
        this.playerName = playerStats.playerName;
        this.level = playerStats.level;
        this.damageModifier = playerStats.damageModifier;
        this.money = playerStats.money;

        this.maxHealth = playerStats.maxHealth;
        this.currentHealth = playerStats.currentHealth;
        this.currentDamage = playerStats.currentDamage;
        this.currentAbilityDamage = playerStats.currentAbilityDamage;
        this.currentCritChance = playerStats.currentCritChance;
        this.currentCritDamage = playerStats.currentCritDamage;
        this.currentDodgeRate = playerStats.currentDodgeRate;
        this.overrideStats = playerStats.overrideStats;
        this.overrideCalculatedHealth = playerStats.overrideCalculatedHealth;
    }
}