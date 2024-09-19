using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, PLAYERATTACKED, PLAYERHEALED, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public PlayerBattleHUD playerHUD;
    public EnemyBattleHUD enemyHUD;

    PlayerStats playerUnit;
    EnemyStats enemyUnit;

    public Text DialogueText;

    public BattleState state;

    float damageModified;
	
	
	public string battleSceneName;  // Name of your battle scene

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<PlayerStats>();
		
		// Use the player's stats stored in GameData
		if (PlayerManager.Instance.playerStats != null)
		{   // Retrieve player stats from gamedataholder
			playerUnit.Name = PlayerManager.Instance.playerStats.Name;
			playerUnit.Level = PlayerManager.Instance.playerStats.Level;
			playerUnit.maxHealth = PlayerManager.Instance.playerStats.maxHealth;
			playerUnit.currentHealth = PlayerManager.Instance.playerStats.currentHealth;
			playerUnit.currentDamage = PlayerManager.Instance.playerStats.currentDamage; 
			playerUnit.currentAbilityDamage = PlayerManager.Instance.playerStats.currentAbilityDamage;
			playerUnit.currentCritChance = PlayerManager.Instance.playerStats.currentCritChance;
			playerUnit.currentCritDamage = PlayerManager.Instance.playerStats.currentCritDamage;
			playerUnit.currentDodgeRate = PlayerManager.Instance.playerStats.currentDodgeRate;
			playerUnit.Money = PlayerManager.Instance.playerStats.Money;

			playerHUD.SetHUD(playerUnit); // Set up the HUD

			GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
			enemyUnit = enemyGO.GetComponent<EnemyStats>();
			enemyUnit.Name = GameDataHolder.enemyStats.Name;
			enemyUnit.Level = GameDataHolder.enemyStats.Level;
			enemyUnit.maxHealth = GameDataHolder.enemyStats.maxHealth;
			enemyUnit.currentHealth = GameDataHolder.enemyStats.currentHealth;
			enemyUnit.currentDamage = GameDataHolder.enemyStats.currentDamage;
			enemyUnit.currentAbilityDamage = GameDataHolder.enemyStats.currentAbilityDamage;
			enemyUnit.currentCritChance = GameDataHolder.enemyStats.currentCritChance;
			enemyUnit.currentCritDamage = GameDataHolder.enemyStats.currentCritDamage;
			enemyUnit.currentDodgeRate = GameDataHolder.enemyStats.currentDodgeRate;
			enemyUnit.damageModifier = GameDataHolder.enemyStats.damageModifier;
			enemyHUD.SetHUD(enemyUnit);

			DialogueText.text = "A shady looking " + enemyUnit.Name + " has snuck up...";
		}
		else
		{
			Debug.LogError("GameData.playerStats is null!");
		}
		
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
	

    IEnumerator PlayerAttack()
    {
        state = BattleState.PLAYERATTACKED;
		
		// Check if the enemy dodges the attack
		float dodgeRoll = Random.Range(0f, 100f);
		if (dodgeRoll <= enemyUnit.currentDodgeRate)
		{
			DialogueText.text = $"{enemyUnit.Name} dodged the attack!";
			yield return new WaitForSeconds(1f);
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
			yield break; // Exit the coroutine if the attack is dodged
		}
		
		// Calculate if the attack is a critical hit
		float critRoll = Random.Range(0f, 100f);
		float damageModified = playerUnit.currentDamage * playerUnit.damageModifier;
	
		if (critRoll <= playerUnit.currentCritChance)
		{
			damageModified *= playerUnit.currentCritDamage; // Apply critical damage multiplier
			DialogueText.text = "Critical hit!";
			yield return new WaitForSeconds(1f);
		}
		
        bool isDead = enemyUnit.TakeDamage(damageModified);

        enemyHUD.SetHP(enemyUnit.currentHealth);
        DialogueText.text = "The attack was successful!";
        //Damage Enemy
        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.WON;
            enemyHUD.SetHP(enemyUnit.currentHealth = 0);
            EndBattle();
            //End Battle
        }
        else
        {  
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            //Enemy Turn
        }
        
        //Check Enemy State
        //Change Battle State based on enemy state
    }

    IEnumerator PlayerHeal()
    {
        state = BattleState.PLAYERHEALED;
        float damageModified = playerUnit.currentAbilityDamage * playerUnit.damageModifier;
        playerUnit.RegainHealth(damageModified);
        playerHUD.SetHP(playerUnit.currentHealth);
        DialogueText.text = "You healed some health!";
        //Heal Self
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        //Enemy Turn
    }
    
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            DialogueText.text = "You won!";
			playerUnit.Money =+ enemyUnit.Reward;
			StartCoroutine(TransitionToOverworld());
        }
        else if (state == BattleState.LOST)
        {
            DialogueText.text = "You were defeated...";
			StartCoroutine(TransitionToOverworld());
        }
    }
	
	IEnumerator TransitionToOverworld()
	{
		PlayerManager.Instance.playerStats.Name = playerUnit.Name;
		PlayerManager.Instance.playerStats.Level= playerUnit.Level;
		PlayerManager.Instance.playerStats.maxHealth = playerUnit.maxHealth;
		PlayerManager.Instance.playerStats.currentHealth = playerUnit.currentHealth;
		PlayerManager.Instance.playerStats.currentDamage = playerUnit.currentDamage; 
		PlayerManager.Instance.playerStats.currentAbilityDamage = playerUnit.currentAbilityDamage;
		PlayerManager.Instance.playerStats.currentCritChance = playerUnit.currentCritChance;
		PlayerManager.Instance.playerStats.currentCritDamage = playerUnit.currentCritDamage;
		PlayerManager.Instance.playerStats.currentDodgeRate = playerUnit.currentDodgeRate;
		PlayerManager.Instance.playerStats.Money = playerUnit.Money;
		
		
		yield return new WaitForSeconds(2f);
		if (PlayerManager.Instance != null)
        {
            Debug.Log("PlayerManager is active with current health: " + PlayerManager.Instance.playerStats.currentHealth);
        }
        else
        {
            Debug.LogError("PlayerManager instance not found!");
        }
		SceneManager.LoadScene("OverworldTestScene", LoadSceneMode.Single);
		PlayerManager.Instance.transform.GetChild(0).gameObject.SetActive(true);
	}

    void PlayerTurn()
    {
        DialogueText.text = "Choose an Action...";
    }

    IEnumerator EnemyTurn()
    {
        DialogueText.text = enemyUnit.Name + " Attacks!";
        yield return new WaitForSeconds(1f);
		
        // Check if the player dodges the attack
		float dodgeRoll = Random.Range(0f, 100f);
		if (dodgeRoll <= playerUnit.currentDodgeRate)
		{
			DialogueText.text = $"You dodged the attack!";
			yield return new WaitForSeconds(1f);
			state = BattleState.PLAYERTURN;
			PlayerTurn();
			yield break; // Exit the coroutine if the attack is dodged
		}
		
		// Calculate if the attack is a critical hit
		float critRoll = Random.Range(0f, 100f);
		float damageModified = enemyUnit.currentDamage * enemyUnit.damageModifier;
	
		if (critRoll <= enemyUnit.currentCritChance)
		{
			damageModified *= enemyUnit.currentCritDamage; // Apply critical damage multiplier
			DialogueText.text = "Critical hit!";
			yield return new WaitForSeconds(1f);
		}
		
        bool isDead = playerUnit.TakeDamage(damageModified);
		
		
        playerHUD.SetHP(playerUnit.currentHealth);
        yield return new WaitForSeconds(1f);
        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
            //End Battle
        }
        else
        {  
            state = BattleState.PLAYERTURN;
            PlayerTurn();
            //Player Turn
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerHeal());
    }
	
	
}

